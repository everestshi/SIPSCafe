using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Sips.Data;
using Sips.Repositories;
using Sips.SipsModels;
using Sips.ViewModels;

namespace Sips.Controllers
{
    public class TransactionController : Controller
    {
        private readonly SipsdatabaseContext _db;
        private PayPalVM payPalVM;
        private readonly IConfiguration _configuration;

        public TransactionController(SipsdatabaseContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            //DbSet<OrderDetail> items = _db.OrderDetails;
            //return View(items);
            TransactionRepo transactionRepo = new TransactionRepo(_db);

            return View(transactionRepo.GetTransactions());
        }
    
        public IActionResult PayPal(PayPalVM payPalVM)
        {
            _db.PayPalVMs.Add(payPalVM);
            _db.SaveChanges();
            return View(payPalVM);
        }
        public IActionResult Checkout()
        {
            HttpContext.Session.SetString("Cart", User.Identity.Name);

            // Other code for PayPal client ID
            var payPalClient = _configuration["PayPal:ClientId"];
            ViewData["PayPalClientId"] = payPalClient;

            return View();

        }

        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] OrderDetail orderDetail)
        {
            try
            {
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(orderDetail);
        }
        // Home page shows list of items.
        // Item price is set through the ViewBag.
        public IActionResult Confirmation(string confirmationId)
        {
            OrderDetail transaction =
            _db.OrderDetails.FirstOrDefault(t => t.OrderDetailId == confirmationId);

            return View("Confirmation", transaction);
        }

        public JsonResult AddToCart(int id)
        {
            string cartSession = HttpContext.Session.GetString("Cart");
            if (cartSession != null)
            {
                List<CheckoutVM> cartItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckoutVM>>(cartSession);
                if (cartItems.Any(c => c.ItemId == id))
                {
                    CheckoutVM cartItem = cartItems.FirstOrDefault(c => c.ItemId == id);
                    int index = cartItems.FindIndex(c => c.ItemId == id);
                    if (index != -1)
                    {
                        cartItems[index] = new CheckoutVM
                        {
                            ItemId = id,
                            Quantity = cartItem.Quantity + 1
                        };
                    }
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
                }
                else
                {
                    cartItems.Add(new CheckoutVM
                    {
                        ItemId = id,
                        Quantity = 1
                    });
                }
            }
            else
            {
                List<CheckoutVM> cartItems = new List<CheckoutVM> { new CheckoutVM { ItemId = id,
                                                                         Quantity = 1 } };
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
            }
            return Json("Success");
        }

    }
}
