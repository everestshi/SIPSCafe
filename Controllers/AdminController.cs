using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        private IEnumerable<Item> products;

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
        public IActionResult ItemIndex(string message, string sortOrder, string searchString, int? pageNumber, int pageSize = 4)
        {
            message = message ?? string.Empty;
            ViewData["Message"] = message;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "idSortDesc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "nameSortDesc" : "Name";



            ProductRepository prorepo = new ProductRepository(_context);
            products = prorepo.GetAll().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) ||
                                        p.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }


            switch (sortOrder)
            {

                case "nameSortDesc":
                    products =
                        products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Name":
                    products =
                        products.OrderBy(p => p.Name).ToList();
                    break;
                case "idSortDesc":
                    products =
                        products.OrderByDescending(p => p.PkItemId).ToList();
                    break;
                default:
                    products =
                        products.OrderBy(p => p.PkItemId).ToList();
                    break;
            }

            int pageIndex = pageNumber ?? 1;
            var count = products.Count();
            var items = products.Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize).ToList();
            var paginatedProducts = new PaginatedList<Item>(items
                                                                , count
                                                                , pageIndex
                                                                , pageSize);



            return View(paginatedProducts);
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
        public IActionResult CustomerIndex(string message)
        {
            message = message ?? string.Empty;

            ViewData["Message"] = message; 
            CustomerRepository customerrepo = new CustomerRepository(_context);
            return View(customerrepo.GetAll());
        }
        public IActionResult CustomerDetails(int id)
        {
            CustomerRepository customerrepo = new CustomerRepository(_context);
            return View(customerrepo.GetById(id));
        }

        public IActionResult CustomerCreate()
        {
            Contact contact = new Contact();
            contact.FkUserTypeId = 1;
            contact.IsDrinkRedeemed = "no";
            //contact.FkUserType = new Credential();
            return View(contact);
        }

        [HttpPost]
        public IActionResult CustomerCreate(Contact contact)
        {
            CustomerRepository customerRepository = new CustomerRepository(_context);
            contact.FkUserType = new Credential();//how to add this to customer
            if (ModelState.IsValid)
            {

                string repoMessage = customerRepository.Add(contact);

                return RedirectToAction("CustomerIndex", new { message = repoMessage });
            }

            return View(contact);
        }

        public IActionResult CustomerEdit(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository(_context);
            Contact contact = customerRepository.GetById(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult CustomerEdit(Contact contact)
        {
            CustomerRepository customerRepository = new CustomerRepository(_context);
            if (ModelState.IsValid)
            {
                string repoMessage = customerRepository.Update(contact);
                return RedirectToAction("CustomerIndex", new { message = repoMessage });
            }
            return View(contact);
        }

        public IActionResult CustomerDelete(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository(_context);

            return View(customerRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult CustomerDelete(Contact contact)
        {
            CustomerRepository customerRepository = new CustomerRepository(_context);

            string repoMessage = customerRepository.Delete(contact.PkUserId);

            return RedirectToAction("CustomerIndex", new { message = repoMessage });
        }





    }
}