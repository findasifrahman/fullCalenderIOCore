using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullCalenderEvent.Models;
using System.IO;
using PagedList;

namespace FullCalenderEvent.Controllers
{
    public class eventFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: eventFiles
        public ActionResult Index(int? page, string SearchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (SearchString != null)
            {
                return View(db.eventFiles.Where(x => x.EventName.Contains(SearchString)).ToList().ToPagedList(pageNumber, pageSize));
            }
            return View(db.eventFiles.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: eventFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventFiles eventFiles = db.eventFiles.Find(id);
            if (eventFiles == null)
            {
                return HttpNotFound();
            }
            return View(eventFiles);
        }

        // GET: eventFiles/Create
        public ActionResult Create()
        {
            ViewBag.evntlist = new SelectList(db.eventDdays.ToList(), "EventDay", "EventDay");
            
            return View();
        }

        // POST: eventFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( eventFiles eventFiles, HttpPostedFileBase fileOther1Doc)
        {
            ViewBag.evntlist = new SelectList(db.eventDdays.ToList(), "EventDay", "EventDay");
            if (ModelState.IsValid)
            {

                string subPath = "~/Uploads/" + eventFiles.EventName ;
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    check_file(fileOther1Doc, eventFiles);

                db.eventFiles.Add(eventFiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventFiles);
        }
        public void check_file(HttpPostedFileBase file, eventFiles ev)
        {
            if (file != null)
            {
                var allowedExtensions = new[] { ".pdf" };
                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                //if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + ext; 
                    var path = Path.Combine(Server.MapPath("~/Uploads/" + ev.EventName ), myfile);
                    //if (type.Contains("spec"))
                    {
                        ev.fileFolder = Path.Combine("~/Uploads/" + ev.EventName, myfile);
                    }


                    file.SaveAs(path);
                }
            }
        }
        // GET: eventFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.evntlist = new SelectList(db.eventDdays.ToList(), "EventDay", "EventDay");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventFiles eventFiles = db.eventFiles.Find(id);
            if (eventFiles == null)
            {
                return HttpNotFound();
            }
            return View(eventFiles);
        }

        // POST: eventFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eventFiles eventFiles,HttpPostedFileBase fileOther1Doc)
        {
            ViewBag.evntlist = new SelectList(db.eventDdays.ToList(), "EventDay", "EventDay");
            var Prevpathdile = Path.Combine(Server.MapPath(db.eventFiles.Where(m => m.Id == eventFiles.Id).Select(m => m.fileFolder).FirstOrDefault()));
            eventFiles.fileFolder = db.eventFiles.Where(m => m.Id == eventFiles.Id).Select(m => m.fileFolder).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathdile) && fileOther1Doc != null)
            {
                System.IO.File.Delete(Prevpathdile);
            }

            string subPath = "~/Uploads/" + eventFiles.EventName;
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            check_file(fileOther1Doc, eventFiles);
            if (ModelState.IsValid)
            {
                db.Entry(eventFiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventFiles);
        }

        // GET: eventFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventFiles eventFiles = db.eventFiles.Find(id);


            var Prevpathfile1 = Path.Combine(Server.MapPath(db.eventFiles.Where(m => m.Id == eventFiles.Id).Select(m => m.fileFolder).FirstOrDefault()));
            eventFiles.fileFolder = db.eventFiles.Where(m => m.Id == eventFiles.Id).Select(m => m.fileFolder).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile1))
            {
                System.IO.File.Delete(Prevpathfile1);
            }


            if (eventFiles == null)
            {
                return HttpNotFound();
            }
            return View(eventFiles);
        }

        // POST: eventFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            eventFiles eventFiles = db.eventFiles.Find(id);
            db.eventFiles.Remove(eventFiles);
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
