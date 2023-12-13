using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sips.Data;
using Sips.Models;
using Sips.Repositories;
using System.Diagnostics;

namespace Sips.Controllers
{
    public class AdminController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly SipsContext _context;


        public AdminController(SipsContext context)
        {
            //_logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Product CRUD**********************************************
        public IActionResult ItemIndex(string message)
        {
            message = message ?? string.Empty;
            ViewData["Message"] = message; 
            ProductRepository prorepo = new ProductRepository(_context);
            return View(prorepo.GetAll());
        }
        public IActionResult ItemDetails(int id)
        {
            ProductRepository prorepo = new ProductRepository(_context);
            return View(prorepo.GetById(id));
        }

        public IActionResult ItemCreate()
        {
            ProductRepository prorepo = new ProductRepository(_context);
            ViewData["Ice"] = new SelectList(_context.Items, "Ice", "Ice");
            ViewData["Sweetness"] = new SelectList(_context.Items, "Sweetness", "Sweetness");
            ViewData["ItemType"] = new SelectList(_context.Items, "ItemType", "ItemType");

            return View();
        }

        [HttpPost]
        public IActionResult ItemCreate(Item item)
        {
            ProductRepository prorepo = new ProductRepository(_context);
            if (ModelState.IsValid)
            {

                string repoMessage = prorepo.Add(item);

                return RedirectToAction("ItemIndex", new { message = repoMessage });
            }

            ViewData["Ice"] = new SelectList(_context.Items, "Ice", "Ice");
            ViewData["Sweetness"] = new SelectList(_context.Items, "Sweetness", "Sweetness");
            ViewData["ItemType"] = new SelectList(_context.Items, "ItemType", "ItemType");


            return View(item);
        }

        public IActionResult ItemEdit(int id)
        {
            ProductRepository prorepo = new ProductRepository(_context);
            Item item = prorepo.GetById(id);
            ViewData["Ice"] = new SelectList(_context.Items, "Ice", "Ice", item.Ice);
            ViewData["Sweetness"] = new SelectList(_context.Items, "Sweetness", "Sweetness", item.Sweetness);
            ViewData["ItemType"] = new SelectList(_context.Items, "ItemType", "ItemType", item.ItemType);

            return View(item);
        }

        [HttpPost]
        public IActionResult ItemEdit(Item item)
        {
            ProductRepository prorepo = new ProductRepository(_context);
            if (ModelState.IsValid)
            {

                string repoMessage = prorepo.Update(item);

                return RedirectToAction("ItemIndex", new { message = repoMessage });
            }

            ViewData["Ice"] = new SelectList(_context.Items, "Ice", "Ice", item.Ice);
            ViewData["Sweetness"] = new SelectList(_context.Items, "Sweetness", "Sweetness", item.Sweetness);
            ViewData["ItemType"] = new SelectList(_context.Items, "ItemType", "ItemType", item.ItemType);


            return View(item);
        }

        public IActionResult ItemDelete(int id)
        {
            ProductRepository prorepo = new ProductRepository(_context);

            return View(prorepo.GetById(id));
        }

        [HttpPost]
        public IActionResult ItemDelete(Item item)
        {
            ProductRepository prorepo = new ProductRepository(_context);

            string repoMessage = prorepo.Delete(item.PkItemId);

            return RedirectToAction("ItemIndex", new { message = repoMessage });
        }




        //Customer CRUD**********************************************
        public IActionResult CustomerIndex()
        {
            CustomerRepository customerrepo = new CustomerRepository(_context);
            return View(customerrepo.GetAll());
        }
        public IActionResult CustomerDetails(int id)
        {
            CustomerRepository customerrepo = new CustomerRepository(_context);
            return View(customerrepo.GetById(id));
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