using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GarbMan.Models;

namespace GarbMan.Controllers
{
    public class ZipChecksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZipChecks
        public ActionResult Index()
        {
            return View(db.ZipChecks.ToList());
        }

        // GET: ZipChecks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCheck zipCheck = db.ZipChecks.Find(id);
            if (zipCheck == null)
            {
                return HttpNotFound();
            }
            return View(zipCheck);
        }

        // GET: ZipChecks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZipChecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Zip")] ZipCheck zipCheck)
        {
            if (ModelState.IsValid)
            {
                db.ZipChecks.Add(zipCheck);
                db.SaveChanges();
                return RedirectToAction("OscarsRoute", "PickUpDays");
            }

            return View(zipCheck);
        }

        // GET: ZipChecks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCheck zipCheck = db.ZipChecks.Find(id);
            if (zipCheck == null)
            {
                return HttpNotFound();
            }
            return View(zipCheck);
        }

        // POST: ZipChecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Zip")] ZipCheck zipCheck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zipCheck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zipCheck);
        }

        // GET: ZipChecks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCheck zipCheck = db.ZipChecks.Find(id);
            if (zipCheck == null)
            {
                return HttpNotFound();
            }
            return View(zipCheck);
        }

        // POST: ZipChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZipCheck zipCheck = db.ZipChecks.Find(id);
            db.ZipChecks.Remove(zipCheck);
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
