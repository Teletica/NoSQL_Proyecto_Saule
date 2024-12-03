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
    public class EmpleadosController : Controller
    {
        private readonly DbContex _context;

        public EmpleadosController()
        {
            _context = new DbContex();
        }

        // GET: Empleados
        public ActionResult Index()
        {
            return View();
        }

        // GET: Empleados/List
        public ActionResult List()
        {
            var empleados = _context.EmpleadoCollection.Find(_ => true).ToList();
            return View(empleados);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(string id)
        {
            var empleado = _context.EmpleadoCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (empleado == null)
            {
                return HttpNotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.EmpleadoCollection.InsertOne(empleado);
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(string id)
        {
            var empleado = _context.EmpleadoCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (empleado == null)
            {
                return HttpNotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Empleado>.Filter.Eq(e => e.Id, id);
                _context.EmpleadoCollection.ReplaceOne(filter, empleado); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(string id)
        {
            var empleado = _context.EmpleadoCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (empleado == null)
            {
                return HttpNotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Empleado>.Filter.Eq(e => e.Id, id);
                _context.EmpleadoCollection.DeleteOne(filter); 

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}