using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly DbContex _context;

        public ProveedoresController()
        {
            _context = new DbContex(); // Asegúrate de que esta conexión esté configurada correctamente para MongoDB.
        }

        // GET: Proveedores
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proveedores/List
        public ActionResult List()
        {
            var proveedores = _context.ProveedorCollection.Find(_ => true).ToList(); // Obtener todos los proveedores
            return View(proveedores);
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(string id)
        {
            var proveedor = _context.ProveedorCollection
                .Find(p => p.Id == id)
                .FirstOrDefault(); // Buscar proveedor por id

            if (proveedor == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(proveedor);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.ProveedorCollection.InsertOne(proveedor); // Insertar nuevo proveedor en MongoDB
                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(string id)
        {
            var proveedor = _context.ProveedorCollection
                .Find(p => p.Id == id)
                .FirstOrDefault(); // Buscar proveedor por id

            if (proveedor == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Verificar si los ids coinciden
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Proveedor>.Filter.Eq(p => p.Id, id); // Filtro para buscar el proveedor por id
                _context.ProveedorCollection.ReplaceOne(filter, proveedor); // Actualizar el proveedor en la base de datos

                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(string id)
        {
            var proveedor = _context.ProveedorCollection
                .Find(p => p.Id == id)
                .FirstOrDefault(); // Buscar proveedor por id

            if (proveedor == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Proveedor>.Filter.Eq(p => p.Id, id); // Filtro para buscar el proveedor por id
                _context.ProveedorCollection.DeleteOne(filter); // Eliminar proveedor

                return RedirectToAction("Index");
            }
            catch
            {
                return View(); // En caso de error, volver a la vista actual
            }
        }
    }
}
