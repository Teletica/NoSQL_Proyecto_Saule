using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class FacturasController : Controller
    {
        private readonly DbContex _context;

        public FacturasController()
        {
            _context = new DbContex(); // Asegúrate de que esta conexión esté configurada correctamente para MongoDB.
        }

        // GET: Facturas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facturas/List
        public ActionResult List()
        {


            var facturas = _context.FacturasCollection.Find(_ => true).ToList(); // Obtener todas las facturas
            return View(facturas);
        }

        // GET: Facturas/Details/5
        public ActionResult Details(string id)
        {
            var factura = _context.FacturasCollection
                .Find(f => f.Id == id)
                .FirstOrDefault(); // Buscar factura por ID

            if (factura == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            // Paso 1: Recuperar datos de Compras desde MongoDB
            var comprasRaw = _context.ComprasCollection
                .Find(_ => true)
                .Project(c => new { c.Id, c.FechaCompra })
                .ToList();

            var compras = comprasRaw
                .Select(c => new SelectListItem
                {
                    Value = c.Id, // El ID de la compra
                    Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}" // Formatear la fecha
                })
                .ToList();

            // Paso 2: Recuperar datos de Ventas desde MongoDB
            var ventasRaw = _context.VentasCollection
                .Find(_ => true)
                .Project(v => new { v.Id, v.TotalNetoVenta })
                .ToList();

            var ventas = ventasRaw
                .Select(v => new SelectListItem
                {
                    Value = v.Id, // El ID de la venta
                    Text = $"Venta ID: {v.Id} - Total Neto: {v.TotalNetoVenta:C}" // Formatear el total neto como moneda
                })
                .ToList();

            // Paso 3: Asignar listas a ViewBag
            ViewBag.Compras = compras;
            ViewBag.Ventas = ventas;

            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        public ActionResult Create(Facturas factura)
        {
            if (ModelState.IsValid)
            {
                _context.FacturasCollection.InsertOne(factura); // Insertar nueva factura en MongoDB
                return RedirectToAction("Index");
            }

            // Paso 1: Recuperar datos de Compras desde MongoDB
            var comprasRaw = _context.ComprasCollection
                .Find(_ => true)
                .Project(c => new { c.Id, c.FechaCompra })
                .ToList();

            var compras = comprasRaw
                .Select(c => new SelectListItem
                {
                    Value = c.Id, // El ID de la compra
                    Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}" // Formatear la fecha
                })
                .ToList();

            // Paso 2: Recuperar datos de Ventas desde MongoDB
            var ventasRaw = _context.VentasCollection
                .Find(_ => true)
                .Project(v => new { v.Id, v.TotalNetoVenta })
                .ToList();

            var ventas = ventasRaw
                .Select(v => new SelectListItem
                {
                    Value = v.Id, // El ID de la venta
                    Text = $"Venta ID: {v.Id} - Total Neto: {v.TotalNetoVenta:C}" // Formatear el total neto como moneda
                })
                .ToList();

            // Paso 3: Asignar listas a ViewBag
            ViewBag.Compras = compras;
            ViewBag.Ventas = ventas;


            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(string id)
        {
            // Paso 1: Recuperar datos de Compras desde MongoDB
            var comprasRaw = _context.ComprasCollection
                .Find(_ => true)
                .Project(c => new { c.Id, c.FechaCompra })
                .ToList();

            var compras = comprasRaw
                .Select(c => new SelectListItem
                {
                    Value = c.Id, // El ID de la compra
                    Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}" // Formatear la fecha
                })
                .ToList();

            // Paso 2: Recuperar datos de Ventas desde MongoDB
            var ventasRaw = _context.VentasCollection
                .Find(_ => true)
                .Project(v => new { v.Id, v.TotalNetoVenta })
                .ToList();

            var ventas = ventasRaw
                .Select(v => new SelectListItem
                {
                    Value = v.Id, // El ID de la venta
                    Text = $"Venta ID: {v.Id} - Total Neto: {v.TotalNetoVenta:C}" // Formatear el total neto como moneda
                })
                .ToList();

            // Paso 3: Asignar listas a ViewBag
            ViewBag.Compras = compras;
            ViewBag.Ventas = ventas;

            var factura = _context.FacturasCollection
                .Find(f => f.Id == id)
                .FirstOrDefault(); // Buscar factura por ID

            if (factura == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(factura);
        }

        // POST: Facturas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Facturas factura)
        {
            if (id != factura.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Verificar si los IDs coinciden
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Facturas>.Filter.Eq(f => f.Id, id); // Filtro para buscar la factura por ID
                _context.FacturasCollection.ReplaceOne(filter, factura); // Actualizar la factura en la base de datos

                return RedirectToAction("Index");
            }


            // Paso 1: Recuperar datos de Compras desde MongoDB
            var comprasRaw = _context.ComprasCollection
                .Find(_ => true)
                .Project(c => new { c.Id, c.FechaCompra })
                .ToList();

            var compras = comprasRaw
                .Select(c => new SelectListItem
                {
                    Value = c.Id, // El ID de la compra
                    Text = $"Compra ID: {c.Id} - Fecha: {c.FechaCompra:dd/MM/yyyy}" // Formatear la fecha
                })
                .ToList();

            // Paso 2: Recuperar datos de Ventas desde MongoDB
            var ventasRaw = _context.VentasCollection
                .Find(_ => true)
                .Project(v => new { v.Id, v.TotalNetoVenta })
                .ToList();

            var ventas = ventasRaw
                .Select(v => new SelectListItem
                {
                    Value = v.Id, // El ID de la venta
                    Text = $"Venta ID: {v.Id} - Total Neto: {v.TotalNetoVenta:C}" // Formatear el total neto como moneda
                })
                .ToList();

            // Paso 3: Asignar listas a ViewBag
            ViewBag.Compras = compras;
            ViewBag.Ventas = ventas;

            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(string id)
        {
            var factura = _context.FacturasCollection
                .Find(f => f.Id == id)
                .FirstOrDefault(); // Buscar factura por ID

            if (factura == null)
            {
                return HttpNotFound(); // Si no se encuentra, devolver "no encontrado"
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Facturas>.Filter.Eq(f => f.Id, id); // Filtro para buscar la factura por ID
                _context.FacturasCollection.DeleteOne(filter); // Eliminar factura

                return RedirectToAction("Index");
            }
            catch
            {
                return View(); // En caso de error, volver a la vista actual
            }
        }
    }
}
