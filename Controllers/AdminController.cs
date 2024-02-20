using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Sips.Data;
using Sips.SipsModels;
using Sips.ViewModels;
using Sips.Repositories;
using System.Diagnostics;

namespace Sips.Controllers
{
    public class AdminController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly SipsdatabaseContext _db;
        private IEnumerable<Item> products;

        public AdminController(SipsdatabaseContext db)
        {
            //_logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        // ROLES INDEX **********************************************

        public IActionResult RoleIndex(string message)
        {
            return View();
        }


        //Product CRUD**********************************************
        public IActionResult ItemIndex(string message, string sortOrder, string searchString, int? pageNumber, int pageSize = 20)
        {
            message = message ?? string.Empty;
            ViewData["Message"] = message;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "idSortDesc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "nameSortDesc" : "Name";



            ProductRepository prorepo = new ProductRepository(_db);
            IEnumerable<ItemVM> products = prorepo.GetAll().ToList();

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
                        products.OrderByDescending(p => p.ItemId).ToList();
                    break;
                default:
                    products =
                        products.OrderBy(p => p.ItemId).ToList();
                    break;
            }

            int pageIndex = pageNumber ?? 1;
            var count = products.Count();
            var items = products.Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize).ToList();
            var paginatedProducts = new PaginatedList<ItemVM>(items
                                                                , count
                                                                , pageIndex
                                                                , pageSize);



            return View(paginatedProducts);
        }
        public IActionResult ItemDetails(int id)
        {
            ProductRepository prorepo = new ProductRepository(_db);
            return View(prorepo.GetById(id));
        }

        public IActionResult ItemCreate()
        {
            ProductRepository prorepo = new ProductRepository(_db);

            //ViewBag.ItemTypes = prorepo.GetItemTypes();
            //ViewBag["ItemType"] = new SelectList(_db.ItemTypes, "itemTypeID", "itemTypeName");
            var itemTypeList = _db.ItemTypes.ToList(); // Make sure it's materialized
            //var DefaultOption = itemVm.ItemType?.ItemTypeName;
            ViewBag.ItemTypes = new SelectList(itemTypeList, "ItemTypeId", "ItemTypeName");

            return View();
        }

        [HttpPost]
        public IActionResult ItemCreate(ItemVM proVM)
        {
            ProductRepository prorepo = new ProductRepository(_db);
            if (ModelState.IsValid)
            {

                string repoMessage = prorepo.Add(proVM);

                return RedirectToAction("ItemIndex", new { message = repoMessage });
            }

            //ViewBag.ItemTypes = prorepo.GetItemTypes();
            var itemTypeList = _db.ItemTypes.ToList(); // Make sure it's materialized
            ViewBag.ItemTypes = new SelectList(itemTypeList, "ItemTypeId", "ItemTypeName");

            return View(proVM);
        }

        public IActionResult ItemEdit(int id)
        {
            ProductRepository prorepo = new ProductRepository(_db);
            ItemVM itemVm = prorepo.GetById(id);

            //ViewBag.ItemType = prorepo.GetItemTypes();

            var itemTypeList = _db.ItemTypes.ToList(); // Make sure it's materialized
            ViewBag.ItemTypes = new SelectList(itemTypeList, "ItemTypeId", "ItemTypeName", itemVm.ItemTypeId);

            return View(itemVm);
        }

        [HttpPost]
        public IActionResult ItemEdit(ItemVM itemVm)
        {
            ProductRepository prorepo = new ProductRepository(_db);
            if (ModelState.IsValid)
            {

                string repoMessage = prorepo.Update(itemVm);

                return RedirectToAction("ItemIndex", new { message = repoMessage });
            }
            var itemTypeList = _db.ItemTypes.ToList(); 
            //ViewBag.ItemTypes = new SelectList(itemTypeList, "ItemTypeId", "ItemTypeName", itemVm.ItemType?.ItemTypeName);
            ViewBag.ItemTypes = new SelectList(itemTypeList, "ItemTypeId", "ItemTypeName", itemVm.ItemTypeId);



            return View(itemVm);
        }

        public IActionResult ItemDelete(int id)
        {
            ProductRepository prorepo = new ProductRepository(_db);

            return View(prorepo.GetById(id));
        }

        [HttpPost]
        public IActionResult ItemDelete(ItemVM item)
        {
            ProductRepository prorepo = new ProductRepository(_db);

            string repoMessage = prorepo.Delete(item.ItemId);

            return RedirectToAction("ItemIndex", new { message = repoMessage });
        }



        //}
        //Customer CRUD**********************************************
        //public IActionResult CustomerIndex(string message)
        //{
        //    message = message ?? string.Empty;

        //    ViewData["Message"] = message; 
        //    CustomerRepository customerrepo = new CustomerRepository(_db);
        //    return View(customerrepo.GetAll());
        //}
        //public IActionResult CustomerDetails(int id)
        //{
        //    CustomerRepository customerrepo = new CustomerRepository(_db);
        //    return View(customerrepo.GetById(id));
        //}

        //public IActionResult CustomerCreate()
        //{
        //    Contact contact = new Contact();
        //    contact.IsDrinkRedeemed = false;
        //    //contact.FkUserType = new Credential();
        //    return View(contact);
        //}

        //[HttpPost]
        //public IActionResult CustomerCreate(Contact contact)
        //{
        //    CustomerRepository customerRepository = new CustomerRepository(_db);
        //    if (ModelState.IsValid)
        //    {

        //        string repoMessage = customerRepository.Add(contact);

        //        return RedirectToAction("CustomerIndex", new { message = repoMessage });
        //    }

        //    return View(contact);
        //}

        //public IActionResult CustomerEdit(int id)
        //{
        //    CustomerRepository customerRepository = new CustomerRepository(_db);
        //    Contact contact = customerRepository.GetById(id);
        //    return View(contact);
        //}

        //[HttpPost]
        //public IActionResult CustomerEdit(Contact contact)
        //{
        //    CustomerRepository customerRepository = new CustomerRepository(_db);
        //    if (ModelState.IsValid)
        //    {
        //        string repoMessage = customerRepository.Update(contact);
        //        return RedirectToAction("CustomerIndex", new { message = repoMessage });
        //    }
        //    return View(contact);
        //}

        //public IActionResult CustomerDelete(int id)
        //{
        //    CustomerRepository customerRepository = new CustomerRepository(_db);

        //    return View(customerRepository.GetById(id));
        //}

        //[HttpPost]
        //public IActionResult CustomerDelete(Contact contact)
        //{
        //    CustomerRepository customerRepository = new CustomerRepository(_db);
        //    //contactUserId = customerRepository

        //    string repoMessage = customerRepository.Delete(contact);

        //    return RedirectToAction("CustomerIndex", new { message = repoMessage });
        //}





    }
}