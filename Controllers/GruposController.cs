using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class GruposController : Controller
    {
        private readonly DbContex _context;

        public GruposController()
        {
            _context = new DbContex(); // Inicializa el contexto de la base de datos.
        }

        // GET: Grupos
        public ActionResult Index()
        {
            return RedirectToAction("List"); // Redirige a la lista de grupos.
        }

        // GET: Grupos/List
        public ActionResult List()
        {
            try
            {
                var grupos = _context.GrupoCollection.Find(_ => true).ToList(); // Obtener todos los grupos
                return View(grupos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener la lista de grupos: {ex.Message}";
                return View("Error");
            }
        }

        // GET: Grupos/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var grupo = _context.GrupoCollection.Find(g => g.Id == id).FirstOrDefault(); // Buscar grupo por ID

                if (grupo == null)
                {
                    return HttpNotFound("Grupo no encontrado.");
                }

                return View(grupo);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del grupo (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            var categorias = _context.CategorialCollection
            .Find(_ => true)
            .Project(c => new SelectListItem
            {
                Value = c.Id.ToString(),  // Convertimos el Id de la categoría a string
                Text = c.nombreCategoria
            })
            .ToList();

            ViewBag.Categorias = categorias;

            return View();
        }

        // POST: Grupos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grupo grupo)
        {
           

            if (ModelState.IsValid)
            {
                _context.GrupoCollection.InsertOne(grupo);
                return RedirectToAction("List");
                
            }

            var categorias = _context.CategorialCollection
            .Find(_ => true)
            .Project(c => new SelectListItem
            {
                Value = c.Id.ToString(),  // Convertimos el Id de la categoría a string
                Text = c.nombreCategoria
            })
            .ToList();

            ViewBag.Categorias = categorias;

            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(string id)
        {
            var categorias = _context.CategorialCollection
             .Find(_ => true)
             .Project(c => new SelectListItem
             {
                 Value = c.Id.ToString(),  // Convertimos el Id de la categoría a string
                 Text = c.nombreCategoria
             })
             .ToList();

            ViewBag.Categorias = categorias;

            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var grupo = _context.GrupoCollection.Find(g => g.Id == id).FirstOrDefault(); // Buscar grupo por ID

                if (grupo == null)
                {
                    return HttpNotFound("Grupo no encontrado.");
                }

                return View(grupo);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener el grupo para edición (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }

        // POST: Grupos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Grupo grupo)
        {
            var categorias = _context.CategorialCollection
            .Find(_ => true)
            .Project(c => new SelectListItem
            {
                Value = c.Id.ToString(),  // Convertimos el Id de la categoría a string
                Text = c.nombreCategoria
            })
            .ToList();

            ViewBag.Categorias = categorias;

            if (id != grupo.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID no coincide.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var filter = Builders<Grupo>.Filter.Eq(g => g.Id, id);
                    _context.GrupoCollection.ReplaceOne(filter, grupo); // Actualizar el grupo en MongoDB
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al actualizar el grupo (ID: {id}): {ex.Message}";
                }
            }

            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var grupo = _context.GrupoCollection.Find(g => g.Id == id).FirstOrDefault(); // Buscar grupo por ID

                if (grupo == null)
                {
                    return HttpNotFound("Grupo no encontrado.");
                }

                return View(grupo);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener el grupo para eliminación (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var filter = Builders<Grupo>.Filter.Eq(g => g.Id, id);
                _context.GrupoCollection.DeleteOne(filter); // Eliminar grupo en MongoDB
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al eliminar el grupo (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }
    }
}
