using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DbContex _context;

        public ClientesController()
        {
            _context = new DbContex();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cliente/List
        public ActionResult List()
        {
            var clientes = _context.ClienteCollection.Find(_ => true).ToList();
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(string id)
        {
            var cliente = _context.ClienteCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
             

                _context.ClienteCollection.InsertOne(cliente);
                return RedirectToAction("Index");
            }


            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(string id)
        {
            var cliente = _context.ClienteCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Cliente>.Filter.Eq(e => e.Id, id);
                _context.ClienteCollection.ReplaceOne(filter, cliente);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(string id)
        {
            var cliente = _context.ClienteCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(e => e.Id, id);
                _context.ClienteCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}