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
    public class MetasController : Controller
    {
        private CMIEntities db = new CMIEntities();

        // GET: Metas
        public ActionResult Index()
        {
            var meta = db.Meta.Include(m => m.Indicador);
            return View(meta.ToList());
        }

        // GET: Metas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meta meta = db.Meta.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }
            return View(meta);
        }

        // GET: Metas/Create
        public ActionResult Create()
        {
            ViewBag.IndicadorID = new SelectList(db.Indicador, "ID", "Nombre");
            return View();
        }

        // POST: Metas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Valor_esperado,Fecha_limite,IndicadorID")] Meta meta)
        {
            if (ModelState.IsValid)
            {
                db.Meta.Add(meta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IndicadorID = new SelectList(db.Indicador, "ID", "Nombre", meta.IndicadorID);
            return View(meta);
        }

        // GET: Metas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meta meta = db.Meta.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IndicadorID = new SelectList(db.Indicador, "ID", "Nombre", meta.IndicadorID);
            return View(meta);
        }

        // POST: Metas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Valor_esperado,Fecha_limite,IndicadorID")] Meta meta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IndicadorID = new SelectList(db.Indicador, "ID", "Nombre", meta.IndicadorID);
            return View(meta);
        }

        // GET: Metas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meta meta = db.Meta.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }
            return View(meta);
        }

        // POST: Metas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meta meta = db.Meta.Find(id);
            db.Meta.Remove(meta);
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
