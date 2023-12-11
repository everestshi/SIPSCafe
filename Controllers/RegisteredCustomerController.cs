using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sips.Data;
using Sips.Models;

namespace Sips.Controllers
{
    [Authorize]
    public class RegisteredCustomerController : Controller
    {
        private readonly SipsContext _context;
        public RegisteredCustomerController(SipsContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            //Session variable for name
            //string customerName = _context.Contacts.FirstOrDefault(c => c.Email == User.Identity.Name)?.FirstName;
            //HttpContext.Session.SetString("SessionUserName", customerName);
            return View();
        }
    }
}
