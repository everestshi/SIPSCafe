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
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

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
            //HttpContext.Session.SetString("Cart", User.Identity.Name);

            // Other code for PayPal client ID
            var payPalClient = _configuration["PayPal:ClientId"];
            ViewData["PayPalClientId"] = payPalClient;
            string cartSession = HttpContext.Session.GetString("Cart");
            List<CartVM> cartItems = new List<CartVM>();

            if (cartSession != null)
            {
                List<CartVM>  sessionCartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartSession);

                decimal total = 0;
                int quantity = 0;
                int lastItemId = 0;

                foreach (var item in sessionCartItems.OrderBy(c => c.ItemId))
                {
                    if (item.ItemId != lastItemId)
                    {
                        lastItemId = item.ItemId;
                        cartItems.Add(item);
                    }
                    else {
                        cartItems.Last().Quantity += item.Quantity;
                        cartItems.Last().Subtotal += item.Subtotal;
                    }
                }
            }
            else
            {
                cartItems = new List<CartVM>();
            }

            return View(cartItems);

        }

        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public IActionResult PaySuccess([FromBody] OrderDetail orderDetail)
        {
            try
            {
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();

                // Save the PaymentId of the PayPalVM item to the session variable as a string
                HttpContext.Session.SetString("PayPalConfirmationModelId", orderDetail.TransactionId);

                // Construct the redirect URL
                var redirectUrl = Url.Action("Transaction", "PayPal");

                // Return a JSON response with the redirect URL
                return Json(new { redirectUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        //Confirmation Page
        public IActionResult Confirmation()
        {
            try
            {
                // Retrieve the PaymentId of the PayPalVM item from the session variable
                var payPalConfirmationModelPaymentId = HttpContext.Session.GetString("PayPalConfirmationModelId");

                if (!string.IsNullOrEmpty(payPalConfirmationModelPaymentId))
                {
                    // Retrieve the PayPalVM item from the database using the PaymentId
                    var orderDetailModel = _db.OrderDetails.FirstOrDefault(model => model.TransactionId == payPalConfirmationModelPaymentId);

                    if (orderDetailModel != null)
                    {
                        // Clear the session variable to ensure it's not available on subsequent requests
                        HttpContext.Session.Remove("PayPalConfirmationModelId");

                        // This action should only be accessed via a server-side redirect, not directly from the client.
                        // If a client tries to access it directly, you may want to handle it appropriately.
                        return View("PayPalConfirmation", orderDetailModel);
                    }
                    else
                    {
                        // Handle the case when the PayPalVM item with the specified PaymentId is not found
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Handle the case when the session variable is empty or not available
                    // Redirect or return an error view as appropriate for your application
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home"); // Redirect to an error view
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public JsonResult GetCartItems(string itemId)
        {
            string cartSession = HttpContext.Session.GetString("Cart");
            List<CartVM> cartItems;

            if (cartSession != null)
            {
                cartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartSession);
                cartItems = cartItems.Where(c => c.ItemId == Int32.Parse(itemId)).ToList();
            }
            else
            {
                cartItems = new List<CartVM>();
            }

            return Json(cartItems);
        }
        
        public JsonResult AddToCart([FromBody] CartVM cartVM)
        {
            string cartSession = HttpContext.Session.GetString("Cart");
            List<CartVM> cartItems;

            if (cartSession != null)
            {
                cartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartSession);
            }
            else
            {
                cartItems = new List<CartVM>();
            }

            // Check if the item is already in the cart
            var existingItem = cartItems.FirstOrDefault(c => c.ItemId == cartVM.ItemId);
            //if (existingItem != null)
            //{
            //    // Update the quantity
            //    existingItem.Quantity = cartVM.Quantity;
            //}
            //else
            //{
                // Add the item to the cart
            cartItems.Add(cartVM);
            //}

            // Serialize and store the updated cart items in the session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            // Return the response
            return Json(new { success = true, message = "Item added/updated in the cart." });
        }

    }
}
