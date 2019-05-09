using FullCalenderEvent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FullCalenderEvent.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? Id)
        {
            var datetime = DateTime.Now;
            ViewBag.curdate = datetime;
            ViewBag.dlist = db.eventDdays.Where(x => DbFunctions.TruncateTime(x.Dday) >= datetime).ToList();
            if(Id != null)
            {
                var cd = db.eventDdays.Where(x => x.Id == Id).FirstOrDefault();
                ViewBag.curdate = cd.Dday;
                return View(db.eventDdays.Where(x => x.Id == Id ).Include(x => x.DdayDetails).ToList());//db.DdayDetails.Where(x=> x.eventDdayId == Id).ToList());
            }
            return View(db.eventDdays.Include(x => x.DdayDetails).ToList());
        }
        public ActionResult archievView()
        {
            return View(db.eventFiles.ToList());
        }
        public ActionResult Indexpa(string dval)
        {
            ViewBag.dlist = db.eventDdays.ToList();
            return View(db.DdayDetails.ToList());//db.eventDdays.ToList());
        }
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventDday eventDday = db.eventDdays.Include(x => x.DdayDetails).FirstOrDefault(x => x.Id == id);//.Find(id);
            if (eventDday == null)
            {
                return HttpNotFound();
            }
            return View(eventDday);
        }
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}