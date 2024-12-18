using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class VentasController : Controller
    {
        private readonly DbContex _context;

        public VentasController()
        {
            _context = new DbContex();
        }

        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ventas/List
        public ActionResult List()
        {
            var ventas = _context.VentasCollection.Find(_ => true).ToList();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public ActionResult Details(string id)
        {
            var venta = _context.VentasCollection
                .Find(v => v.Id == id)
                .FirstOrDefault();

            if (venta == null)
            {
                return HttpNotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            // Cargar las compras disponibles para asociarlas a la venta
            var compras = _context.ComprasCollection
                                   .Find(_ => true)
                                   .ToList()
                                   .Select(c => new SelectListItem
                                   {
                                       Value = c.Id,
                                       Text = c.FechaCompra.ToString("dd/MM/yyyy")
                                   })
                                   .ToList();
            ViewBag.Compras = compras;

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.VentasCollection.InsertOne(venta);
                return RedirectToAction("Index");
            }

            // Si hay un error, recargamos la lista de compras disponibles
            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .ToList()
                                  .Select(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = c.FechaCompra.ToString("dd/MM/yyyy")
                                  })
                                  .ToList();
            ViewBag.Compras = compras;

            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(string id)
        {
            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .ToList()
                                  .Select(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = c.FechaCompra.ToString("dd/MM/yyyy")
                                  })
                                  .ToList();
            ViewBag.Compras = compras;

            var venta = _context.VentasCollection
                .Find(v => v.Id == id)
                .FirstOrDefault();

            if (venta == null)
            {
                return HttpNotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Venta venta)
        {
            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .ToList()
                                  .Select(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = c.FechaCompra.ToString("dd/MM/yyyy")
                                  })
                                  .ToList();
            ViewBag.Compras = compras;

            if (id != venta.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Venta>.Filter.Eq(v => v.Id, id);
                _context.VentasCollection.ReplaceOne(filter, venta); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(string id)
        {
            var venta = _context.VentasCollection
                .Find(v => v.Id == id)
                .FirstOrDefault();

            if (venta == null)
            {
                return HttpNotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Venta>.Filter.Eq(v => v.Id, id);
                _context.VentasCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
