using Microsoft.AspNetCore.Mvc;
using Sips.Data;
using Sips.Models;
using System.Diagnostics;

namespace Sips.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SipsContext _context;


        public AdminController(ILogger<HomeController> logger, SipsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }


    }
}