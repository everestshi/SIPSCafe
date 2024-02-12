using Microsoft.AspNetCore.Mvc;
using Sips.SipsModels;
using System.Diagnostics;

namespace Sips.Controllers
{
    public class HomeController : Controller
    {
        private readonly SipsdatabaseContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(SipsdatabaseContext context, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Contact ()
        {
            return View();
        }
    }
}