using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using NoSQL_Proyecto_Saule.Models;

namespace NoSQL_Proyecto_Saule.Controllers
{
    public class MarcasController : Controller
    {
        private readonly DbContex _context;

        public MarcasController()
        {
            _context = new DbContex();
        }

        // GET: Marcas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Marcas/List
        public ActionResult List()
        {
            var marcas = _context.MarcaCollection.Find(_ => true).ToList();
            return View(marcas);
        }

        // GET: Marcas/Details/5
        public ActionResult Details(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Marca marca, HttpPostedFileBase imgLogoMarcaFile)
        {
            if (ModelState.IsValid)
            {
                if (imgLogoMarcaFile != null && imgLogoMarcaFile.ContentLength > 0)
                {
                    // Asegúrate de que el archivo esté llegando correctamente
                    System.Diagnostics.Debug.WriteLine("File received: " + imgLogoMarcaFile.FileName);
                    using (var binaryReader = new BinaryReader(imgLogoMarcaFile.InputStream))
                    {
                        marca.imgLogoMarca = binaryReader.ReadBytes(imgLogoMarcaFile.ContentLength);
                    }
                }
                else
                {
                    // Si no hay archivo, agrega un mensaje de depuración
                    System.Diagnostics.Debug.WriteLine("No file received.");
                }

                _context.MarcaCollection.InsertOne(marca);
                return RedirectToAction("Index");
            }

            return View(marca);
        }


        // GET: Marcas/Edit/5
        public ActionResult Edit(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Marca marca, HttpPostedFileBase imgLogoMarcaFile)
        {
            if (id != marca.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (imgLogoMarcaFile != null && imgLogoMarcaFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(imgLogoMarcaFile.InputStream))
                    {
                        marca.imgLogoMarca = binaryReader.ReadBytes(imgLogoMarcaFile.ContentLength);
                    }
                }
                else
                {
                    // Si no se ha subido un nuevo archivo, mantenemos la imagen actual
                    var existingMarca = _context.MarcaCollection.Find(e => e.Id == id).FirstOrDefault();
                    marca.imgLogoMarca = existingMarca?.imgLogoMarca;
                }

                var filter = Builders<Marca>.Filter.Eq(e => e.Id, id);
                _context.MarcaCollection.ReplaceOne(filter, marca); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(string id)
        {
            var marca = _context.MarcaCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (marca == null)
            {
                return HttpNotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var filter = Builders<Marca>.Filter.Eq(e => e.Id, id);
                _context.MarcaCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
