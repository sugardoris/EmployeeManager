using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact(string lang)
        {
            switch (lang)
            {
                case "en":
                    ViewBag.Title = "Contact";
                    ViewBag.Message = "Contact us for more information or any questions you may have:";
                    ViewBag.Address = "Address:";
                    ViewBag.Phone = "Phone:";
                    break;
                case "hr":
                    ViewBag.Title = "Kontakt";
                    ViewBag.Message = "Za više informacija i za bilo kakva pitanja kontaktirajte nas:";
                    ViewBag.Address = "Adresa:";
                    ViewBag.Phone = "Telefon:";
                    break;
                default:
                    ViewBag.Title = "Kontakt";
                    ViewBag.Message = "Za više informacija i za bilo kakva pitanja kontaktirajte nas:";
                    ViewBag.Address = "Adresa:";
                    ViewBag.Phone = "Telefon:";
                    break;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}