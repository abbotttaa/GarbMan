using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GarbMan.Models;
using Microsoft.AspNet.Identity;

namespace GarbMan.Controllers
{
    public class PickUpDaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickUpDays
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();

            var detailData = from dingus in db.PickUpDaysModels
                             where (dingus.User == id)
                             select dingus;

            PickUpDaysModels viewTempData = new PickUpDaysModels();
            foreach (PickUpDaysModels result in detailData)
            {
                viewTempData = result;
            }

            if (viewTempData.User == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(viewTempData);

            //string userId = User.Identity.GetUserId();

            //return View(db.PickUpDaysModels.Contains(userId);
        }

        // GET: PickUpDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpDaysModels pickUpDaysModels = db.PickUpDaysModels.Find(id);
            if (pickUpDaysModels == null)
            {
                return HttpNotFound();
            }
            return View(pickUpDaysModels);
        }

        // GET: PickUpDays/Create
        public ActionResult Create()
        {

            string userId = User.Identity.GetUserId();

            var detailData = from wp in db.PickUpDaysModels
                             where (wp.User == userId)
                             select wp;
            int count = detailData.Count();
            if(count == 0)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: PickUpDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PickUpDaysModels pickUpDaysModels)
        {
            if (ModelState.IsValid)
            {
                pickUpDaysModels.User = User.Identity.GetUserId();
                int PickupQuantity = 0;
                DateTime startString = Convert.ToDateTime(pickUpDaysModels.Start);
                DateTime endString = Convert.ToDateTime(pickUpDaysModels.End);
                DateTime skipDay = Convert.ToDateTime(pickUpDaysModels.OneDay);
                DateTime nextMonth = DateTime.Now.AddMonths(1);

                for (DateTime month = DateTime.Now; month.Date <= nextMonth; month = month.Date.AddDays(1))
                {
                    var monthDayCheck = month.DayOfWeek;
                    var skipDayString = skipDay.DayOfWeek;
                    if (monthDayCheck == pickUpDaysModels.PickUpDay)
                    {
                        PickupQuantity++;
                    }

                    if (monthDayCheck == skipDayString && month.Date == skipDay)
                    {
                        PickupQuantity--;
                    }
                }
                for (DateTime date = startString; date.Date <= endString.Date; date = date.AddDays(1))
                {
                    var dateCheck = date.DayOfWeek;
                    if (dateCheck == pickUpDaysModels.PickUpDay)
                    {
                        PickupQuantity--;
                    }
                }
                pickUpDaysModels.Balance = PickupQuantity * 7;
                db.PickUpDaysModels.Add(pickUpDaysModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickUpDaysModels);
        }

        // GET: PickUpDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpDaysModels pickUpDaysModels = db.PickUpDaysModels.Find(id);
            if (pickUpDaysModels == null)
            {
                return HttpNotFound();
            }
            return View(pickUpDaysModels);
        }

        // POST: PickUpDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PickUpDay")] PickUpDaysModels pickUpDaysModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickUpDaysModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickUpDaysModels);
        }

        // GET: PickUpDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpDaysModels pickUpDaysModels = db.PickUpDaysModels.Find(id);
            if (pickUpDaysModels == null)
            {
                return HttpNotFound();
            }
            return View(pickUpDaysModels);
        }

        // POST: PickUpDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickUpDaysModels pickUpDaysModels = db.PickUpDaysModels.Find(id);
            db.PickUpDaysModels.Remove(pickUpDaysModels);
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

        [HttpGet]
        public ActionResult OscarsRoute()
        {
            PickUpDaysModels zipQualified = new PickUpDaysModels();

            var ZipList = db.ZipChecks.ToList();

            int zippy = ZipList[(ZipList.Count) - (1)].Zip;


            var todayZipCheck = from wp in db.PickUpDaysModels
                                where (wp.Zip == zippy)
                                select wp;
            List<PickUpDaysModels> qualifiedPickUp = new List<PickUpDaysModels>();
            foreach (PickUpDaysModels result in todayZipCheck)
            {
                DateTime startString = Convert.ToDateTime(result.Start);
                DateTime endString = Convert.ToDateTime(result.End);
                DateTime skipDay = Convert.ToDateTime(result.OneDay);

                if (skipDay == DateTime.Now)
                {

                }
                else if (startString <= DateTime.Now && endString >= DateTime.Now)
                {

                }
                else if (DateTime.Now.DayOfWeek != result.PickUpDay)
                {

                }
                else
                {
                    zipQualified = result;
                    qualifiedPickUp.Add(zipQualified);
                }
            }
            return View(qualifiedPickUp);
        }

        public ActionResult ApiTest()
        {
            PickUpDaysModels zipQualified = new PickUpDaysModels();

            var ZipList = db.ZipChecks.ToList();

            int zippy = ZipList[(ZipList.Count) - (1)].Zip;


            var todayZipCheck = from wp in db.PickUpDaysModels
                                where (wp.Zip == zippy)
                                select wp;
            List<PickUpDaysModels> qualifiedPickUp = new List<PickUpDaysModels>();
            foreach (PickUpDaysModels result in todayZipCheck)
            {
                DateTime startString = Convert.ToDateTime(result.Start);
                DateTime endString = Convert.ToDateTime(result.End);
                DateTime skipDay = Convert.ToDateTime(result.OneDay);

                if (skipDay == DateTime.Now)
                {

                }
                else if (startString <= DateTime.Now && endString >= DateTime.Now)
                {

                }
                else if (DateTime.Now.DayOfWeek != result.PickUpDay)
                {

                }
                else
                {
                    zipQualified = result;
                    qualifiedPickUp.Add(zipQualified);
                }
            }
            return View(qualifiedPickUp);
        }

       
        [HttpGet]
        public ActionResult GiveZipCode()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GiveZipCode(ZipCheck zipcheck)
        //{
        //    db.Zipcheck.Add(zipcheck);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public List<PickUpDaysModels> PickUpsForTodayByZip()
        //{
            
        //}
    }
}
