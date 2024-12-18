using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
            var grupos = _context.GrupoCollection
                             .Find(_ => true)
                             .Project(g => new SelectListItem
                             {
                                 Value = g.Id,
                                 Text = g.nombrGrupo // Asegúrate de que 'Nombre' es el nombre del grupo
                             })
                             .ToList();
            ViewBag.Grupos = grupos;

            // Cargar las marcas disponibles
            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .Project(m => new SelectListItem
                                 {
                                     Value = m.Id,
                                     Text = m.nombreMarca // Asegúrate de que 'Nombre' es el nombre de la marca
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            // Cargar los proveedores disponibles
            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .Project(p => new SelectListItem
                                      {
                                          Value = p.Id,
                                          Text = p.NombreProveedor // Asegúrate de que 'Nombre' es el nombre del proveedor
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto producto)
        {

            if (ModelState.IsValid)
            {
               
                _context.ProductoCollection.InsertOne(producto);
                return RedirectToAction("Index");
                
            }

            var grupos = _context.GrupoCollection
                         .Find(_ => true)
                         .ToList()
                         .Select(g => new SelectListItem
                         {
                             Value = g.Id.ToString(),
                             Text = g.nombrGrupo
                         })
                         .ToList();
            ViewBag.Grupos = grupos;

            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .ToList()
                                 .Select(m => new SelectListItem
                                 {
                                     Value = m.Id.ToString(),
                                     Text = m.nombreMarca
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .ToList()
                                      .Select(p => new SelectListItem
                                      {
                                          Value = p.Id.ToString(),
                                          Text = p.NombreProveedor
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(string id)
        {
            var grupos = _context.GrupoCollection
                             .Find(_ => true)
                             .Project(g => new SelectListItem
                             {
                                 Value = g.Id,
                                 Text = g.nombrGrupo // Asegúrate de que 'Nombre' es el nombre del grupo
                             })
                             .ToList();
            ViewBag.Grupos = grupos;

            // Cargar las marcas disponibles
            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .Project(m => new SelectListItem
                                 {
                                     Value = m.Id,
                                     Text = m.nombreMarca // Asegúrate de que 'Nombre' es el nombre de la marca
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            // Cargar los proveedores disponibles
            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .Project(p => new SelectListItem
                                      {
                                          Value = p.Id,
                                          Text = p.NombreProveedor // Asegúrate de que 'Nombre' es el nombre del proveedor
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            var producto = _context.ProductoCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Producto producto)
        {
            var grupos = _context.GrupoCollection
                         .Find(_ => true)
                         .ToList()
                         .Select(g => new SelectListItem
                         {
                             Value = g.Id.ToString(),
                             Text = g.nombrGrupo
                         })
                         .ToList();
            ViewBag.Grupos = grupos;

            var marcas = _context.MarcaCollection
                                 .Find(_ => true)
                                 .ToList()
                                 .Select(m => new SelectListItem
                                 {
                                     Value = m.Id.ToString(),
                                     Text = m.nombreMarca
                                 })
                                 .ToList();
            ViewBag.Marcas = marcas;

            var proveedores = _context.ProveedorCollection
                                      .Find(_ => true)
                                      .ToList()
                                      .Select(p => new SelectListItem
                                      {
                                          Value = p.Id.ToString(),
                                          Text = p.NombreProveedor
                                      })
                                      .ToList();
            ViewBag.Proveedores = proveedores;

            if (id != producto.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Producto>.Filter.Eq(p => p.Id, id);
                _context.ProductoCollection.ReplaceOne(filter, producto); // Actualiza el documento

                return RedirectToAction("Index");
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

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Producto>.Filter.Eq(p => p.Id, id);
                _context.ProductoCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}