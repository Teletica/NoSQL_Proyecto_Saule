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
    public class RecetasController : Controller
    {
        private readonly DbContex _context;

        public RecetasController()
        {
            _context = new DbContex(); // Asegúrate de que esta conexión esté configurada correctamente para MongoDB.
        }

        // GET: Recetas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Recetas/List
        public ActionResult List()
        {
            var recetas = _context.RecetasCollection.Find(_ => true).ToList(); // Obtener todas las recetas
            return View(recetas);
        }

        // GET: Recetas/Details/5
        public ActionResult Details(string id)
        {
            var receta = _context.RecetasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar receta por id

            if (receta == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(receta);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recetas/Create
        [HttpPost]
        public ActionResult Create(Receta receta)
        {
            if (ModelState.IsValid)
            {
                _context.RecetasCollection.InsertOne(receta); // Insertar nueva receta en MongoDB
                return RedirectToAction("Index");
            }

            return View(receta);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(string id)
        {
            var receta = _context.RecetasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar receta por id

            if (receta == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(receta);
        }

        // POST: Recetas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Receta receta)
        {
            if (id != receta.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Verificar si los ids coinciden
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Receta>.Filter.Eq(r => r.Id, id); // Filtro para buscar la receta por id
                _context.RecetasCollection.ReplaceOne(filter, receta); // Actualizar la receta en la base de datos

                return RedirectToAction("Index");
            }

            return View(receta);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(string id)
        {
            var receta = _context.RecetasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar receta por id

            if (receta == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Receta>.Filter.Eq(r => r.Id, id); // Filtro para buscar la receta por id
                _context.RecetasCollection.DeleteOne(filter); // Eliminar receta

                return RedirectToAction("Index");
            }
            catch
            {
                return View(); // En caso de error, volver a la vista actual
            }
        }
    }
}
