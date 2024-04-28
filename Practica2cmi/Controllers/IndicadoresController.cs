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
    public class IndicadoresController : Controller
    {
        private CMIEntities db = new CMIEntities();

        // GET: Indicadores
        public ActionResult Index()
        {
            var indicador = db.Indicador.Include(i => i.Objetivo).Include(i => i.Tipo);
            return View(indicador.ToList());
        }

        // GET: Indicadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // GET: Indicadores/Create
        public ActionResult Create()
        {
            ViewBag.ObjetivoID = new SelectList(db.Objetivo, "ID", "Descripcion");
            ViewBag.TipoID = new SelectList(db.Tipo, "ID", "Nombre");
            return View();
        }

        // POST: Indicadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,ObjetivoID,TipoID")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                db.Indicador.Add(indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObjetivoID = new SelectList(db.Objetivo, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipo, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // GET: Indicadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObjetivoID = new SelectList(db.Objetivo, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipo, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // POST: Indicadores/Edit/5
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
            ViewBag.ObjetivoID = new SelectList(db.Objetivo, "ID", "Descripcion", indicador.ObjetivoID);
            ViewBag.TipoID = new SelectList(db.Tipo, "ID", "Nombre", indicador.TipoID);
            return View(indicador);
        }

        // GET: Indicadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // POST: Indicadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador indicador = db.Indicador.Find(id);
            db.Indicador.Remove(indicador);
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
