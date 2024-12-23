﻿using System;
using System.Net;
using System.Web.Mvc;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System.Collections.Generic;
using System.Linq;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class ComprasController : Controller
    {
        private readonly DbContex _context;

        public ComprasController()
        {
            _context = new DbContex(); // Inicializa el contexto
        }
    

        // GET: Compras
        public ActionResult Index()
        {
            try
            {
                var compras = _context.ComprasCollection.Find(_ => true).ToList();
                return View(compras); // Retorna la vista Index con datos.
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener la lista de compras: {ex.Message}";
                return View("Error"); // Retorna una vista de error en caso de excepción.
            }
        }


        // GET: Compras/List
        public ActionResult List()
        {
            try
            {
                var compras = _context.ComprasCollection.Find(_ => true).ToList();
                return View(compras);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener la lista de compras: {ex.Message}";
                return View("Error");
            }
        }

        // GET: Compras/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID no válido.");
            }

            try
            {
                var compra = _context.ComprasCollection.Find(e => e.Id == id).FirstOrDefault();

                if (compra == null)
                {
                    return HttpNotFound("Compra no encontrada.");
                }

                return View(compra);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles de la compra: {ex.Message}";
                return View("Error");
            }
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compras compra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ComprasCollection.InsertOne(compra);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al crear la compra: {ex.Message}";
                }
            }

            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID no válido.");
            }

            try
            {
                var compra = _context.ComprasCollection.Find(e => e.Id == id).FirstOrDefault();

                if (compra == null)
                {
                    return HttpNotFound("Compra no encontrada.");
                }

                return View(compra);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener la compra para edición: {ex.Message}";
                return View("Error");
            }
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Compras compra, string ProductosCompra)
        {
            if (id != compra.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID no coincide.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Convierte la cadena a una lista de productos
                    if (!string.IsNullOrEmpty(ProductosCompra))
                    {
                        compra.ProductosCompra = ProductosCompra.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                                 .Select(p => p.Trim())
                                                                 .ToList();
                    }
                    else
                    {
                        compra.ProductosCompra = new List<string>();
                    }

                    var filter = Builders<Compras>.Filter.Eq(e => e.Id, id);
                    _context.ComprasCollection.ReplaceOne(filter, compra);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al actualizar la compra: {ex.Message}";
                }
            }

            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID no válido.");
            }

            try
            {
                var compra = _context.ComprasCollection.Find(e => e.Id == id).FirstOrDefault();

                if (compra == null)
                {
                    return HttpNotFound("Compra no encontrada.");
                }

                return View(compra);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener la compra para eliminación: {ex.Message}";
                return View("Error");
            }
        }

        // POST: Compras/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var filter = Builders<Compras>.Filter.Eq(e => e.Id, id);
                _context.ComprasCollection.DeleteOne(filter);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al eliminar la compra: {ex.Message}";
                return View("Error");
            }
        }
    }
}
