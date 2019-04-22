using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FullCalenderIOMVCCore.Data;
using FullCalenderIOMVCCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace FullCalenderIOMVCCore.Controllers
{
    public class eventDdaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public eventDdaysController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: eventDdays
        public async Task<IActionResult> Index()
        {
            return View(await  _context.eventDday.Include(xx => xx.DdayDetails).ToListAsync());
            //return View(await _context.eventDday.ToListAsync());
        }
        public ActionResult allEventsbyid(int? id)
        {
            var events = _context.eventDday.Include(xx => xx.DdayDetails).FirstOrDefault(x => x.Id == id);
            return Ok(events);
            //return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult dDayDetails()
        {
            var events = _context.DdayDetails.ToList();
            return Ok(events);
            //return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult liss()
        {

            var dt = DateTime.Now.Date;

            var dateRes = _context.eventDday.Where(x => x.Dday.Date == dt).FirstOrDefault();
            if (dateRes != null)
            {
                return Ok (new { result = "success", timea = dateRes.Dday.ToShortTimeString() });
                //return new JsonResult { Data = (new { result = "success", timea = dateRes.Dday.ToShortTimeString() }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Ok(new { result = "fail" });
            //return new JsonResult { Data = (new { result = "fail" }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: eventDdays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDday = await _context.eventDday
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventDday == null)
            {
                return NotFound();
            }

            return View(eventDday);
        }
        [Authorize]
        // GET: eventDdays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: eventDdays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(eventDday eventDday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventDday);
                await _context.SaveChangesAsync();
                return Json(new { status = true, baz = "Blech" });
                //return RedirectToAction(nameof(Index));
            }
            return Json(new { status = false, baz = "Blech" });
            //return View(eventDday);
        }
        [Authorize]
        // GET: eventDdays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDday = await _context.eventDday.SingleOrDefaultAsync(m => m.Id == id);
            if (eventDday == null)
            {
                return NotFound();
            }
            return View(eventDday);
        }

        // POST: eventDdays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int mid,eventDday eventDday)
        {
            Debug.WriteLine("-----------------------------");
            Debug.WriteLine(eventDday.Dday);
            Debug.WriteLine(eventDday.Id);
            Debug.WriteLine("-----------------------------");
            if (ModelState.IsValid)
            {
                //////
                var eventDdays = _context.eventDday.SingleOrDefault(m => m.Id == mid);
                _context.eventDday.Remove(eventDdays);
                _context.SaveChanges();
                //////
                try
                {
                    _context.Add(eventDday);
                    await _context.SaveChangesAsync();
                    return Json(new { status = true, baz = "Blech" });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!eventDdayExists(eventDday.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return Json(new { status = false, baz = "Blech" });
            //return View(eventDday);
        }

        // GET: eventDdays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDday = await _context.eventDday
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventDday == null)
            {
                return NotFound();
            }

            return View(eventDday);
        }

        // POST: eventDdays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventDday = await _context.eventDday.SingleOrDefaultAsync(m => m.Id == id);
            _context.eventDday.Remove(eventDday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool eventDdayExists(int id)
        {
            return _context.eventDday.Any(e => e.Id == id);
        }
    }
}
