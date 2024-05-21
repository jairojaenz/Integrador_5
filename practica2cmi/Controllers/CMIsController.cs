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
    public class CMIsController : Controller
    {
        private CMIDBContext db = new CMIDBContext();

        // GET: CMIs
        public ActionResult Index()
        {
            return View(db.CMIs.ToList());
        }

        // GET: CMIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMI cMI = db.CMIs.Find(id);
            if (cMI == null)
            {
                return HttpNotFound();
            }
            return View(cMI);
        }

        // GET: CMIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CMIs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Nombre,Periodo")] CMI cMI)
        {
            if (ModelState.IsValid)
            {
                db.CMIs.Add(cMI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMI);
        }

        // GET: CMIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMI cMI = db.CMIs.Find(id);
            if (cMI == null)
            {
                return HttpNotFound();
            }
            return View(cMI);
        }

        // POST: CMIs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Nombre,Periodo")] CMI cMI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMI);
        }

        // GET: CMIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMI cMI = db.CMIs.Find(id);
            if (cMI == null)
            {
                return HttpNotFound();
            }
            return View(cMI);
        }

        // POST: CMIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CMI cMI = db.CMIs.Find(id);
            db.CMIs.Remove(cMI);
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
