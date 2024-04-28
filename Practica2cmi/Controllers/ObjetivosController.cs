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
    public class ObjetivosController : Controller
    {
        private CMIEntities db = new CMIEntities();

        // GET: Objetivos
        public ActionResult Index()
        {
            var objetivo = db.Objetivo.Include(o => o.CMI).Include(o => o.Perspectiva);
            return View(objetivo.ToList());
        }

        // GET: Objetivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivos/Create
        public ActionResult Create()
        {
            ViewBag.CMIID = new SelectList(db.CMI, "ID", "Descripcion");
            ViewBag.PerspectivaID = new SelectList(db.Perspectiva, "ID", "Nombre");
            return View();
        }

        // POST: Objetivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,CMIID,PerspectivaID")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivo.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CMIID = new SelectList(db.CMI, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectiva, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CMIID = new SelectList(db.CMI, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectiva, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,CMIID,PerspectivaID")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CMIID = new SelectList(db.CMI, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectiva, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // GET: Objetivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objetivo objetivo = db.Objetivo.Find(id);
            db.Objetivo.Remove(objetivo);
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
