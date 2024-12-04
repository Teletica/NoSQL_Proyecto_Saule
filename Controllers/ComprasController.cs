using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class ComprasController : Controller
    {
        private readonly DbContex _context;

        public ComprasController()
        {
            _context = new DbContex();
        }

        // GET: Compras
        public ActionResult Index()
        {
            return View();
        }

        // GET: Compras/List
        public ActionResult List()
        {
            var compras = _context.ComprasCollection.Find(_ => true).ToList();
            return View(compras);
        }

        // GET: Compras/Details/5
        public ActionResult Details(string id)
        {
            var compra = _context.ComprasCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (compra == null)
            {
                return HttpNotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        public ActionResult Create(Compras compra)
        {
            if (ModelState.IsValid)
            {
                _context.ComprasCollection.InsertOne(compra);
                return RedirectToAction("Index");
            }

            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(string id)
        {
            var compra = _context.ComprasCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (compra == null)
            {
                return HttpNotFound();
            }

            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Compras compra)
        {
            if (id != compra.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Compras>.Filter.Eq(e => e.Id, id);
                _context.ComprasCollection.ReplaceOne(filter, compra);

                return RedirectToAction("Index");
            }

            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(string id)
        {
            var compra = _context.ComprasCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (compra == null)
            {
                return HttpNotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Compras>.Filter.Eq(e => e.Id, id);
                _context.ComprasCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
