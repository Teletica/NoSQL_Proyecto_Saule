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
    public class MarcasController : Controller
    {
        private readonly DbContex _context;

        public MarcasController()
        {
            _context = new DbContex();
        }

        // GET: Marcas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Marcas/List
        public ActionResult List()
        {
            var marcas = _context.MarcaCollection.Find(_ => true).ToList();
            return View(marcas);
        }

        // GET: Marcas/Details/5
        public ActionResult Details(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        public ActionResult Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.MarcaCollection.InsertOne(marca);
                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marcas/Edit/5
        public ActionResult Edit(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Marca marca)
        {
            if (id != marca.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Marca>.Filter.Eq(e => e.Id, id);
                _context.MarcaCollection.ReplaceOne(filter, marca); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Marca>.Filter.Eq(e => e.Id, id);
                _context.MarcaCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
