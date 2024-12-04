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
    public class RolesController : Controller
    {
        private readonly DbContex _context;

        public RolesController()
        {
            _context = new DbContex(); // Asegúrate de que esta conexión esté configurada correctamente para MongoDB.
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        // GET: Roles/List
        public ActionResult List()
        {
            var roles = _context.RolCollection.Find(_ => true).ToList(); // Obtener todos los roles
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var rol = _context.RolCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar rol por id

            if (rol == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(rol);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.RolCollection.InsertOne(rol); // Insertar nuevo rol en MongoDB
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var rol = _context.RolCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar rol por id

            if (rol == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(rol);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Rol rol)
        {
            if (id != rol.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Verificar si los ids coinciden
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Rol>.Filter.Eq(r => r.Id, id); // Filtro para buscar el rol por id
                _context.RolCollection.ReplaceOne(filter, rol); // Actualizar el rol en la base de datos

                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var rol = _context.RolCollection
                .Find(r => r.Id == id)
                .FirstOrDefault(); // Buscar rol por id

            if (rol == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(rol);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Rol>.Filter.Eq(r => r.Id, id); // Filtro para buscar el rol por id
                _context.RolCollection.DeleteOne(filter); // Eliminar rol

                return RedirectToAction("Index");
            }
            catch
            {
                return View(); // En caso de error, volver a la vista actual
            }
        }
    }
}
