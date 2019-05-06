using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullCalenderEvent.Models;
using System.Diagnostics;
using PagedList;

namespace FullCalenderEvent.Controllers
{
    public class eventDdaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: eventDdays
        public ActionResult Index(int? page, string SearchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (SearchString != null)
            {
                return View(db.eventDdays.Where(x=> x.EventDay.Contains(SearchString)).ToList().ToPagedList(pageNumber, pageSize));
            }
            return View(db.eventDdays.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult allEventsbyid(int? id)
        {
            var events = db.eventDdays.Include(xx => xx.DdayDetails).FirstOrDefault(x=> x.Id == id);
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return View(db.eventDdays.Include(xx=> xx.DdayDetails).ToList());
        }
        public ActionResult dDayDetails()
        {
            var events = db.DdayDetails.ToList();//db.eventDdays.Include(xx=> xx.DdayDetails).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return View(db.eventDdays.Include(xx=> xx.DdayDetails).ToList());
        }
        public ActionResult liss()
        {
            //return Json(new { foo = "bar", baz = "Blech" });
            var dt = DateTime.Now;
            
            var dateRes = db.eventDdays.Where(x => DbFunctions.TruncateTime(x.Dday) == DbFunctions.TruncateTime(dt)).FirstOrDefault();
            if (dateRes != null) { 
                return new JsonResult { Data = (new { result = "success", timea = dateRes.Dday.ToShortTimeString() }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = (new { result = "fail"}), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: eventDdays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventDday eventDday = db.eventDdays.Include(xx => xx.DdayDetails).FirstOrDefault(x=> x.Id== id);//.Find(id);
            if (eventDday == null)
            {
                return HttpNotFound();
            }
            return View(eventDday);
        }
        [Authorize]
        // GET: eventDdays/Create
        public ActionResult Create(string ename)
        {
            string colorname = "";
            if(ename == "DDNT") { colorname = "dodgerblue";  }
            else if (ename == "SOTI") { colorname = "red"; }
            else if (ename == "SOTII") { colorname = "darkblue"; }
            else if (ename == "OICTDEC") { colorname = "green"; }
            else if (ename == "PA") { colorname = "orange"; }
            ViewBag.colorval = colorname;
            return View();
        }

        // POST: eventDdays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(eventDday eventDday)
        {
           // string colorname = "";
           // ViewBag.colorval = eventDday.DdayDetails;

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Debug.WriteLine(error);
                }
            }
            Debug.WriteLine("-----------------------------");
            if (ModelState.IsValid)
            {
                db.eventDdays.Add(eventDday);
                db.SaveChanges();
                return Json(new { status = true, baz = "Blech" });
                //return RedirectToAction("Index");
            }
            return Json(new { status = false, baz = "Blech" });
            //return View(eventDday);
        }
        [Authorize]
        // GET: eventDdays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbcol = db.DdayDetails.Where(xx => xx.eventDdayId == id).FirstOrDefault();
            if (dbcol != null)
            {
                ViewBag.colorname = dbcol.Color;
            }
            else
            {
               // ViewBag.colorname
            }
            
            eventDday eventDday = db.eventDdays.Include(xx => xx.DdayDetails).FirstOrDefault(x => x.Id == id);//.Find(id);
            if (eventDday == null)
            {
                return HttpNotFound();
            }
            return View(eventDday);
        }

        // POST: eventDdays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(eventDday eventDday)
        {
            Debug.WriteLine("-----------------------------");
            Debug.WriteLine(eventDday.Dday);
            Debug.WriteLine(eventDday.Id);
            Debug.WriteLine("-----------------------------");
            if (ModelState.IsValid)
            {
                /////////////////
                eventDday eventDdayrm = db.eventDdays.Find(eventDday.Id);
                db.eventDdays.Remove(eventDdayrm);
                db.SaveChanges();
                //////////////////
                db.eventDdays.Add(eventDday);
                db.SaveChanges();
                return Json(new { status = true, baz = "Blech" });
                //return RedirectToAction("Index");

            }
            return Json(new { status = false, baz = "Blech" });
            // return View(eventDday);
        }
        [Authorize]
        // GET: eventDdays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventDday eventDday = db.eventDdays.Find(id);
            if (eventDday == null)
            {
                return HttpNotFound();
            }
            return View(eventDday);
        }
        [Authorize]
        // POST: eventDdays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            eventDday eventDday = db.eventDdays.Find(id);
            db.eventDdays.Remove(eventDday);
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
