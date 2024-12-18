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

            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Producto producto)
        {
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