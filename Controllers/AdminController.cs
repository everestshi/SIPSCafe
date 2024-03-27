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
        //private IEnumerable<Item> products;

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
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceSortDesc" : "Price";
            ViewData["InventorySortParm"] = sortOrder == "Inventory" ? "InventorySortDesc" : "Inventory";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "TypeSortDesc" : "Type";




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
                case "InventorySortDesc":
                    products = products.OrderByDescending(p => p.Inventory).ToList();
                    break;
                case "Inventory":
                    products = products.OrderBy(p => p.Inventory).ToList();
                    break;

                case "TypeSortDesc":
                    products = products.OrderByDescending(p => p.ItemTypeName).ToList();
                    break;
                case "Type":
                    products = products.OrderBy(p => p.ItemTypeName).ToList();
                    break;

                case "PriceSortDesc":
                    products = products.OrderByDescending(p => p.BasePrice).ToList();
                    break;
                case "Price":
                    products = products.OrderBy(p => p.BasePrice).ToList();
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
                string repoMessage = prorepo.AddAsync(proVM).Result;

                //string repoMessage =string( prorepo.AddAsync(proVM));
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
            //ViewBag.ImageFile = itemVm.ImageFile;

            return View(itemVm);
        }

        [HttpPost]
        public IActionResult ItemEdit(ItemVM itemVm)
        {
            ProductRepository prorepo = new ProductRepository(_db);
            if (ModelState.IsValid)
            {

                string repoMessage = prorepo.UpdateAsync(itemVm).Result;
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
                string userEmail = User.Identity.Name;
                var user = _db.Contacts.Where(c => c.Email == userEmail).FirstOrDefault();

                if (user != null)
                {
                    string userFirstName = user.FirstName;

                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetString("SessionUserName", userFirstName);
                    }
                }
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


        // Order Detail CRUD********************************************************************

        public IActionResult OrderIndex(string message, string sortOrder, string searchString, int? pageNumber, int pageSize = 20)
        {
            message = message ?? string.Empty;
            ViewData["Message"] = message;
            OrderDetailRepo orderDetailRepo = new OrderDetailRepo(_db);
            IEnumerable<OrderDetailVM> ordersVM = orderDetailRepo.GetAll().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                ordersVM = ordersVM.Where(c => c.UserEmail.ToLower().Contains(searchString.ToLower()) ||
                                               c.ItemTypes.Select(item => item.ToLower()).Contains(searchString.ToLower()) ||
                                                c.StoreId.ToString().Contains(searchString)



                                        ).ToList();
            }

            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "idSortDesc" : "";
            ViewData["StorIDSortParm"] = sortOrder == "StrorID" ? "StrorIDDesc" : "StrorID";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "EmailSortDesc" : "Email";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "DateDesc" : "Date";
            ViewData["TotalPriceSortParm"] = sortOrder == "TotalPrice" ? "TotalPriceDesc" : "TotalPrice";


            switch (sortOrder)
            {
                case "idSortDesc":
                    ordersVM = ordersVM.OrderByDescending(p => p.TransactionId).ToList();
                    break;
                case "StrorIDDesc":
                    ordersVM = ordersVM.OrderByDescending(p => p.StoreId).ToList();
                    break;
                case "StrorID":
                    ordersVM = ordersVM.OrderBy(p => p.StoreId).ToList();
                    break;

                case "TotalPriceDesc":
                    ordersVM = ordersVM.OrderByDescending(p => p.totalPrice).ToList();
                    break;
                case "TotalPrice":
                    ordersVM = ordersVM.OrderBy(p => p.totalPrice).ToList();
                    break;

                case "DateDesc":
                    ordersVM = ordersVM.OrderByDescending(p => p.DateOrdered).ToList();
                    break;
                case "Date":
                    ordersVM = ordersVM.OrderBy(p => p.DateOrdered).ToList();
                    break;

                case "EmailSortDesc":
                    ordersVM = ordersVM.OrderByDescending(p => p.UserEmail).ToList();
                    break;
                case "Email":
                    ordersVM = ordersVM.OrderBy(p => p.UserEmail).ToList();
                    break;


                default:
                    ordersVM = ordersVM.OrderBy(p => p.TransactionId).ToList();
                    break;
            }

            int pageIndex = pageNumber ?? 1;
            var count = ordersVM.Count();
            var items = ordersVM.Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize).ToList();
            var paginatedOrders = new PaginatedList<OrderDetailVM>(items
                                                                , count
                                                                , pageIndex
                                                                , pageSize);

            return View(paginatedOrders);


        }


        public IActionResult OrderDetails(string id)
        {
            OrderDetailRepo orderRepo = new OrderDetailRepo(_db);
            return View(orderRepo.GetById(id));
        }

        public IActionResult OrderDelete(string id)
        {
            OrderDetailRepo orderRepo = new OrderDetailRepo(_db);
            return View(orderRepo.GetById(id));
        }

        [HttpPost]
        public IActionResult OrderDelete(OrderDetailVM orderVM)
        {
            OrderDetailRepo orderRepo = new OrderDetailRepo(_db);
            string repoMessage = orderRepo.Delete(orderVM);
            return RedirectToAction("OrderIndex", new { message = repoMessage });
        }



        //images********************************************************************************

        public IActionResult ImageIndex()
        {
            return View();
        }

        public IActionResult Images()
        {
            IEnumerable<ImageStore> images = _db.ImageStores;

            return View(images);
        }

        public IActionResult ImageSave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImageSave(UploadModel uploadModel)
        {
            if (ModelState.IsValid)
            {
                if (uploadModel.ImageFile != null && uploadModel.ImageFile.Length > 0)
                {
                    string contentType = uploadModel.ImageFile.ContentType;

                    if (contentType == "image/png" ||
                        contentType == "image/jpeg" ||
                        contentType == "image/jpg")
                    {
                        try
                        {
                            byte[] imageData;

                            using (var memoryStream = new MemoryStream())
                            {
                                await uploadModel.ImageFile.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }

                            var image = new ImageStore
                            {
                                FileName = Path.
                                   GetFileNameWithoutExtension(uploadModel.ImageFile.FileName),
                                Image = imageData
                            };

                            _db.ImageStores.Add(image);

                            await _db.SaveChangesAsync();
                            return RedirectToAction("ImageIndex", "Admin");

                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("imageUpload"
                                                    , "An error occured uploading your image."
                                                    + " Please try again.");
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("imageUpload", "Please upload a PNG, " +
                                                                "JPG, or JPEG file.");
                    }
                }
                else
                {
                    ModelState.AddModelError("imageUpload", "Please select an " +
                                                            " image to upload.");
                }
            }

            return View(uploadModel);
        }


    }
}