using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly DbContex _context;

        public CategoriasController()
        {
            _context = new DbContex();
        }

        // GET: Categorias
        public ActionResult Index()
        {
            return View();
        }

        // GET: Categorias/List
        public ActionResult List()
        {
            var categorias = _context.CategorialCollection.Find(_ => true).ToList();
            return View(categorias);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(string id)
        {
            var categoria = _context.CategorialCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.CategorialCollection.InsertOne(categoria);
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(string id)
        {
            var categoria = _context.CategorialCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Categoria>.Filter.Eq(e => e.Id, id);
                _context.CategorialCollection.ReplaceOne(filter, categoria); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(string id)
        {
            var categoria = _context.CategorialCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Categoria>.Filter.Eq(e => e.Id, id);
                _context.CategorialCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
