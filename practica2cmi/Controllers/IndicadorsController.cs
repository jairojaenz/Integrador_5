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
    public class IndicadorsController : Controller
    {
        private CMIDBContext db = new CMIDBContext();

        // GET: Indicadors
        public ActionResult Index()
        {
            var indicadors = db.Indicadors.Include(i => i.Objetivo).Include(i => i.Tipo);
            return View(indicadors.ToList());
        }

        // GET: Indicadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // GET: Indicadors/Create
        public ActionResult Create()
        {
            ViewBag.ObjetivoID = new SelectList(db.Objetivoes, "ID", "Descripcion");
            ViewBag.TipoID = new SelectList(db.Tipoes, "ID", "Nombre");
            return View();
        }

        // POST: Indicadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,ObjetivoID,TipoID")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                db.Indicadors.Add(indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObjetivoID = new SelectList(db.Objetivoes, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipoes, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // GET: Indicadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObjetivoID = new SelectList(db.Objetivoes, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipoes, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // POST: Indicadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,ObjetivoID,TipoID")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObjetivoID = new SelectList(db.Objetivoes, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipoes, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // GET: Indicadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // POST: Indicadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador indicador = db.Indicadors.Find(id);
            db.Indicadors.Remove(indicador);
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
