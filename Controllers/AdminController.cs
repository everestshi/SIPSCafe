﻿using Microsoft.AspNetCore.Mvc;
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
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Name":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "idSortDesc":
                    products = products.OrderByDescending(p => p.ItemId).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.ItemId).ToList();
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
            var itemTypeList = _db.ItemTypes.ToList(); 
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

            var itemTypeList = _db.ItemTypes.ToList(); 
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


        //Customer CRUD**********************************************
        public IActionResult ContactIndex(string message, string sortOrder, string searchString, int? pageNumber, int pageSize = 20)
        {
            message = message ?? string.Empty;
            ViewData["Message"] = message;
            ViewData["CurrentSort"] = sortOrder;

            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "idSortDesc" : "";
            ViewData["FNameSortParm"] = sortOrder == "FirstName" ? "fnameSortDesc" : "FirstName";
            ViewData["LNameSortParm"] = sortOrder == "LastName" ? "lnameSortDesc" : "LastName";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "EmailSortDesc" : "Email";
            ViewData["ProvinceSortParm"] = sortOrder == "Province" ? "provinceSortDesc" : "Province";
            ViewData["CitySortParm"] = sortOrder == "City" ? "CitySortDesc" : "City";

            ContactRepo contactRepo = new ContactRepo(_db);
            IEnumerable<ContactVM> contacts = contactRepo.GetAll().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(c => c.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                          c.LastName.ToLower().Contains(searchString.ToLower()) ||
                                          c.City.ToLower().Contains(searchString.ToLower()) ||
                                          c.Province.ToLower().Contains(searchString.ToLower()) ||
                                          c.Email.ToLower().Contains(searchString.ToLower()) ||
                                          c.Street.ToLower().Contains(searchString.ToLower()) 

                                        ).ToList();
            }

            switch (sortOrder)
            {

                case "fnameSortDesc":
                    contacts = contacts.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "FirstName":
                    contacts = contacts.OrderBy(p => p.FirstName).ToList();
                    break;

                case "lnameSortDesc":
                    contacts = contacts.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "LastName":
                    contacts = contacts.OrderBy(p => p.LastName).ToList();
                    break;

                case "EmailSortDesc":
                    contacts = contacts.OrderByDescending(p => p.Email).ToList();
                    break;
                case "Email":
                    contacts = contacts.OrderBy(p => p.Email).ToList();
                    break;

                case "ProvinceSortDesc":
                    contacts = contacts.OrderByDescending(p => p.Province).ToList();
                    break;
                case "Province":
                    contacts = contacts.OrderBy(p => p.Province).ToList();
                    break;
                case "CitySortDesc":
                    contacts = contacts.OrderByDescending(p => p.City).ToList();
                    break;
                case "City":
                    contacts = contacts.OrderBy(p => p.City).ToList();
                    break;
                case "idSortDesc":
                    contacts = contacts.OrderByDescending(p => p.UserId).ToList();
                    break;
                default:
                    contacts = contacts.OrderBy(p => p.UserId).ToList();
                    break;
            }
            int pageIndex = pageNumber ?? 1;
            var count = contacts.Count();
            var items = contacts.Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize).ToList();
            var paginatedContacts = new PaginatedList<ContactVM>(items
                                                                , count
                                                                , pageIndex
                                                                , pageSize);

            return View(paginatedContacts);
        }


        public IActionResult ContactDetails(int id)
        {
            ContactRepo contactRepo = new ContactRepo(_db);
            return View(contactRepo.GetById(id));
        }

        public IActionResult ContactCreate()
        {
            Contact contact = new Contact();
            contact.IsDrinkRedeemed = false;
            return View(contact);
        }

        [HttpPost]
        public IActionResult ContactCreate(Contact contact)
        {
            ContactRepo customerRepository = new ContactRepo(_db);
            if (ModelState.IsValid)
            {

                string repoMessage = customerRepository.Add(contact);

                return RedirectToAction("ContactIndex", new { message = repoMessage });
            }

            return View(contact);
        }

        public IActionResult ContactEdit(int id)
        {
            ContactRepo contactRepo = new ContactRepo(_db);
            ContactVM contactVM = contactRepo.GetById(id);
            return View(contactVM);
        }

        [HttpPost]
        public IActionResult ContactEdit(ContactVM contactVM)
        {
            ContactRepo customerrepo = new ContactRepo(_db);
            if (ModelState.IsValid)
            {
                string repoMessage = customerrepo.Update(contactVM);
                return RedirectToAction("ContactIndex", new { message = repoMessage });
            }
            return View(contactVM);
        }

        public IActionResult ContactDelete(int id)
        {
            ContactRepo contactRepo = new ContactRepo(_db);

            return View(contactRepo.GetById(id));
        }

        [HttpPost]
        public IActionResult ContactDelete(ContactVM contactVM)
        {
            ContactRepo contactRepo = new ContactRepo(_db);

            string repoMessage = contactRepo.Delete(contactVM);

            return RedirectToAction("ContactIndex", new { message = repoMessage });
        }

    }
}