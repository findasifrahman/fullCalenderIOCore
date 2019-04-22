using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FullCalenderIOMVCCore.Models;
using FullCalenderIOMVCCore.Data;

namespace FullCalenderIOMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? Id)
        {
            ViewBag.dlist = _context.eventDday.ToList();
            if (Id != null)
            {
                return View(_context.DdayDetails.Where(x => x.eventDdayId == Id).ToList());
            }
            return View(_context.DdayDetails.ToList());//db.eventDdays.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
