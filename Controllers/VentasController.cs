using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System.Collections.Generic;
using System.Linq;
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
            var ventas = _context.VentasCollection.Find(_ => true).ToList();
            return View(ventas ?? new List<Venta>());
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
            var venta = _context.VentasCollection.Find(v => v.Id == id).FirstOrDefault();
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }


        // GET: Ventas/Create
        public ActionResult Create()
        {
            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .ToList() // Traer datos a memoria
                                  .Select(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}"
                                  })
                                  .ToList();

            ViewBag.Compras = compras;

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venta venta)
        {
            if (ModelState.IsValid)
            {
                // Validar que el IdCompra sea un ObjectId válido
                if (!ObjectId.TryParse(venta.IdCompra, out _))
                {
                    ModelState.AddModelError("IdCompra", "El ID de la compra debe ser un ObjectId válido.");
                    return View(venta);
                }

                // Calcular TotalNetoVenta = (TotalBrutoVenta - DescuentoVenta) + ImpuestoVenta
                venta.TotalNetoVenta = (venta.TotalBrutoVenta - (decimal)venta.DescuentoVenta) + (decimal)venta.ImpuestoVenta;

                _context.VentasCollection.InsertOne(venta);
                return RedirectToAction("List");
            }

            // Re-cargar dropdown si el modelo no es válido
            var compras = _context.ComprasCollection
                      .Find(_ => true)
                      .ToList()
                      .Select(c => new SelectListItem
                      {
                          Value = c.Id,
                          Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}"
                      })
                      .ToList();
            ViewBag.Compras = compras;


            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(string id)
        {
            var venta = _context.VentasCollection.Find(v => v.Id == id).FirstOrDefault();
            if (venta == null)
            {
                return HttpNotFound();
            }

            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .ToList() // Traer datos a memoria
                                  .Select(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}"
                                  })
                                  .ToList();

            ViewBag.Compras = compras;

            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Venta venta)
        {
            if (id != venta.Id)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                // Validar que el IdCompra sea un ObjectId válido
                if (!ObjectId.TryParse(venta.IdCompra, out _))
                {
                    ModelState.AddModelError("IdCompra", "El ID de la compra debe ser un ObjectId válido.");
                    return View(venta);
                }

                // Recalcular TotalNetoVenta
                venta.TotalNetoVenta = (venta.TotalBrutoVenta - (decimal)venta.DescuentoVenta) + (decimal)venta.ImpuestoVenta;

                var filter = Builders<Venta>.Filter.Eq(v => v.Id, id);
                _context.VentasCollection.ReplaceOne(filter, venta);
                return RedirectToAction("List");
            }

            // Re-cargar dropdown si el modelo no es válido
            var compras = _context.ComprasCollection
                                  .Find(_ => true)
                                  .Project(c => new SelectListItem
                                  {
                                      Value = c.Id,
                                      Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra.ToShortDateString()}"
                                  })
                                  .ToList();
            ViewBag.Compras = compras;

            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(string id)
        {
            var venta = _context.VentasCollection.Find(v => v.Id == id).FirstOrDefault();
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
            var filter = Builders<Venta>.Filter.Eq(v => v.Id, id);
            _context.VentasCollection.DeleteOne(filter);
            return RedirectToAction("List");
        }
    }
}
