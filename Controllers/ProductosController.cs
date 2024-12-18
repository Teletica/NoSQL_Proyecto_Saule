using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DbContex _context;

        public ProductosController()
        {
            _context = new DbContex();
        }

        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/List
        public ActionResult List()
        {
            // Obtener todos los productos desde MongoDB
            var productos = _context.ProductoCollection.Find(_ => true).ToList();
            return View(productos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(string id)
        {
            var producto = _context.ProductoCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            // Cargar listas desplegables de Grupos
            var grupos = _context.GrupoCollection
                             .Find(_ => true)
                             .Project(g => new SelectListItem
                             {
                                 Value = g.Id,
                                 Text = g.nombrGrupo // Asegúrate de que 'nombrGrupo' es el nombre correcto
                             })
                             .ToList();
            ViewBag.Grupos = grupos;

            // Cargar listas desplegables de Marcas
            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .Project(m => new SelectListItem
                                 {
                                     Value = m.Id,
                                     Text = m.nombreMarca // Asegúrate de que 'nombreMarca' es el nombre correcto
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            // Cargar listas desplegables de Proveedores
            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .Project(p => new SelectListItem
                                      {
                                          Value = p.Id,
                                          Text = p.NombreProveedor // Asegúrate de que 'NombreProveedor' es el nombre correcto
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Asignar valores a las propiedades del producto
                producto.IdGrupo = new IdContainer { Id = producto.IdGrupo.Id }; // Asignar Id del Grupo
                producto.IdMarca = new IdContainer { Id = producto.IdMarca.Id }; // Asignar Id de la Marca
                producto.IdProveedor = new IdContainer { Id = producto.IdProveedor.Id }; // Asignar Id del Proveedor

                // Guardar el producto en MongoDB
                _context.ProductoCollection.InsertOne(producto); // Insertar el producto en la colección de MongoDB

                // Redirigir a la vista de la lista de productos
                return RedirectToAction("List");
            }

            // Recargar las listas desplegables en caso de que haya algún error en el modelo
            ViewBag.Grupos = _context.GrupoCollection
                             .Find(_ => true)
                             .Project(g => new SelectListItem
                             {
                                 Value = g.Id,
                                 Text = g.nombrGrupo
                             })
                             .ToList();
            ViewBag.Marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .Project(m => new SelectListItem
                                 {
                                     Value = m.Id,
                                     Text = m.nombreMarca
                                 })
                                 .ToList();
            ViewBag.Proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .Project(p => new SelectListItem
                                      {
                                          Value = p.Id,
                                          Text = p.NombreProveedor
                                      })
                                      .ToList();

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(string id)
        {
            var producto = _context.ProductoCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (producto == null)
            {
                return HttpNotFound();
            }

            // Cargar las listas desplegables de Grupos, Marcas y Proveedores
            var grupos = _context.GrupoCollection
                                 .Find(_ => true)
                                 .Project(g => new SelectListItem
                                 {
                                     Value = g.Id,
                                     Text = g.nombrGrupo // Asegúrate de que 'nombrGrupo' es el nombre correcto
                                 })
                                 .ToList();
            ViewBag.Grupos = grupos;

            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .Project(m => new SelectListItem
                                 {
                                     Value = m.Id,
                                     Text = m.nombreMarca // Asegúrate de que 'nombreMarca' es el nombre correcto
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .Project(p => new SelectListItem
                                      {
                                          Value = p.Id,
                                          Text = p.NombreProveedor // Asegúrate de que 'NombreProveedor' es el nombre correcto
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            return View(producto);
        }


        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Producto producto)
        {
            if (id != producto.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                // Actualizar producto en la base de datos
                var filter = Builders<Producto>.Filter.Eq(p => p.Id, id);
                _context.ProductoCollection.ReplaceOne(filter, producto);

                return RedirectToAction("List");
            }

            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(string id)
        {
            var producto = _context.ProductoCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (producto == null)
            {
                return HttpNotFound();
            }

            // Obtener el nombre del Grupo
            var grupo = _context.GrupoCollection
                .Find(g => g.Id == producto.IdGrupo.Id)
                .FirstOrDefault();

            // Obtener el nombre de la Marca
            var marca = _context.MarcaCollection
                .Find(m => m.Id == producto.IdMarca.Id)
                .FirstOrDefault();

            // Obtener el nombre del Proveedor
            var proveedor = _context.ProveedorCollection
                .Find(p => p.Id == producto.IdProveedor.Id)
                .FirstOrDefault();

            // Asignar los nombres a las propiedades del modelo para mostrarlos en la vista
            if (grupo != null) producto.NombreGrupo = grupo.nombrGrupo;
            if (marca != null) producto.NombreMarca = marca.nombreMarca;
            if (proveedor != null) producto.NombreProveedor = proveedor.NombreProveedor;

            return View(producto);
        }


        // POST: Productos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var filter = Builders<Producto>.Filter.Eq(p => p.Id, id);
            _context.ProductoCollection.DeleteOne(filter);

            return RedirectToAction("List");
        }
    }
}
