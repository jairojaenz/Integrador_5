using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practica2cmi.Models;

namespace Practica2cmi.Controllers
{
    public class Indicador_DatosController : Controller
    {
        private CMIDBContext db = new CMIDBContext();

        // GET: Indicador_Datos
        public ActionResult Index()
        {
            var indicador_Datos = db.Indicador_Datos.Include(i => i.Indicador);
            return View(indicador_Datos.ToList());
        }

        // GET: Indicador_Datos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador_Datos indicador_Datos = db.Indicador_Datos.Find(id);
            if (indicador_Datos == null)
            {
                return HttpNotFound();
            }
            return View(indicador_Datos);
        }

        // GET: Indicador_Datos/Create
        public ActionResult Create()
        {
            ViewBag.IndicadorID = new SelectList(db.Indicadors, "ID", "Nombre");
            return View();
        }

        // POST: Indicador_Datos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Valor,Fecha,IndicadorID")] Indicador_Datos indicador_Datos)
        {
            if (ModelState.IsValid)
            {
                db.Indicador_Datos.Add(indicador_Datos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IndicadorID = new SelectList(db.Indicadors, "ID", "Nombre", indicador_Datos.IndicadorID);
            return View(indicador_Datos);
        }

        // GET: Indicador_Datos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador_Datos indicador_Datos = db.Indicador_Datos.Find(id);
            if (indicador_Datos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IndicadorID = new SelectList(db.Indicadors, "ID", "Nombre", indicador_Datos.IndicadorID);
            return View(indicador_Datos);
        }

        // POST: Indicador_Datos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Valor,Fecha,IndicadorID")] Indicador_Datos indicador_Datos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicador_Datos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IndicadorID = new SelectList(db.Indicadors, "ID", "Nombre", indicador_Datos.IndicadorID);
            return View(indicador_Datos);
        }

        // GET: Indicador_Datos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador_Datos indicador_Datos = db.Indicador_Datos.Find(id);
            if (indicador_Datos == null)
            {
                return HttpNotFound();
            }
            return View(indicador_Datos);
        }

        // POST: Indicador_Datos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador_Datos indicador_Datos = db.Indicador_Datos.Find(id);
            db.Indicador_Datos.Remove(indicador_Datos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
