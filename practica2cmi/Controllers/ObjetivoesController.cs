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
    public class ObjetivoesController : Controller
    {
        private CMIDBContext db = new CMIDBContext();

        // GET: Objetivoes
        public ActionResult Index()
        {
            var objetivoes = db.Objetivoes.Include(o => o.CMI).Include(o => o.Perspectiva);
            return View(objetivoes.ToList());
        }

        // GET: Objetivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivoes/Create
        public ActionResult Create()
        {
            ViewBag.CMIID = new SelectList(db.CMIs, "ID", "Descripcion");
            ViewBag.PerspectivaID = new SelectList(db.Perspectivas, "ID", "Nombre");
            return View();
        }

        // POST: Objetivoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,CMIID,PerspectivaID")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivoes.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CMIID = new SelectList(db.CMIs, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectivas, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // GET: Objetivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CMIID = new SelectList(db.CMIs, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectivas, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // POST: Objetivoes/Edit/5
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
            ViewBag.CMIID = new SelectList(db.CMIs, "ID", "Descripcion", objetivo.CMIID);
            ViewBag.PerspectivaID = new SelectList(db.Perspectivas, "ID", "Nombre", objetivo.PerspectivaID);
            return View(objetivo);
        }

        // GET: Objetivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objetivo objetivo = db.Objetivoes.Find(id);
            db.Objetivoes.Remove(objetivo);
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
