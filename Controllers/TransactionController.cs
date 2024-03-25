﻿using Microsoft.AspNetCore.Cors.Infrastructure;
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
        //[HttpPost]
        //public IActionResult PaySuccess([FromBody] OrderDetail orderDetail)
        //{
        //    try
        //    {
        //        _db.OrderDetails.Add(orderDetail);
        //        _db.SaveChanges();

        //        // Save the PaymentId of the PayPalVM item to the session variable as a string
        //        HttpContext.Session.SetString("PayPalConfirmationModelId", orderDetail.TransactionId);

        //        // Construct the redirect URL
        //        var redirectUrl = Url.Action("Transaction", "PayPal");

        //        // Return a JSON response with the redirect URL
        //        return Json(new { redirectUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500);
        //    }
        //}

        [HttpPost]
        public IActionResult PaySuccess([FromBody] PayPalVM payPalVM)
        {

            try
            {
                // Get the email of the currently logged-in user
                var userEmail = User.Identity.Name;

                // Find the user in the Contact table based on the email
                var user = _db.Contacts.FirstOrDefault(c => c.Email == userEmail);


                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Construct the Transaction object using properties from the OrderDetail
                var transaction = new Transaction
                {
                    TransactionId = payPalVM.TransactionId,
                    DateOrdered = payPalVM.CreatedDate, // Assuming CreatedDate is in correct format
                    StoreId = 1,
                    UserId = user.UserId
                };
                _db.Transactions.Add(transaction);

                // Retrieve cart data from the session
                var cartJson = HttpContext.Session.GetString("Cart");
                var cartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartJson);

                // Parse the cart items and create OrderDetail objects
                foreach (var cartItem in cartItems)
                {
                    var cartOrderDetail = new OrderDetail
                    {
                        TransactionId = payPalVM.TransactionId, // Assuming TransactionId is already set in OrderDetail
                                                                // Map properties from the CartVM to the OrderDetail
                        ItemId = cartItem.ItemId,
                        Price = cartItem.BasePrice,
                        Quantity = cartItem.Quantity,
                        // Map other properties as needed
                    };
                    _db.OrderDetails.Add(cartOrderDetail);
                }


                // Save the PaymentId of the PayPalVM item to the session variable as a string
                HttpContext.Session.SetString("PayPalConfirmationModelId", payPalVM.TransactionId);

                // Construct the redirect URL
                var redirectUrl = Url.Action("Confirmation", "Transaction");

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
            //var existingItem = cartItems.FirstOrDefault(c => c.ItemId == cartVM.ItemId);
            //if (existingItem != null)
            //{
            //    // Update the quantity
            //    existingItem.Quantity = cartVM.Quantity;
            //}
            //else
            //{
            // Add the item to the cart
            cartVM.ItemIdQuantity = cartVM.ItemId + "-" + cartVM.Quantity;

            cartItems.Add(cartVM);
            //}

            // Serialize and store the updated cart items in the session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            // Return the response
            return Json(new { success = true, message = "Item added/updated in the cart." });
        }

        public JsonResult AddOneToCart([FromBody] string itemIdQuantity)
        {
            string cartSession = HttpContext.Session.GetString("Cart");
            List<CartVM> cartItems;

            if (cartSession != null)
            {
                cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartVM>>(cartSession);
            }
            else
            {
                cartItems = new List<CartVM>();
            }
            string itemId = itemIdQuantity.Split('-')[0];
            string quantity = itemIdQuantity.Split('-')[1];
            // Check if the item is already in the cart
            var existingItem = cartItems.FirstOrDefault(c => c.ItemIdQuantity == itemIdQuantity);
            if (existingItem != null)
            {
                // Add to cart
                // Parse existing quantity to int, increment by 1, and convert back to string
                int existingQuantity = int.Parse(existingItem.Quantity.ToString());
                int newQuantity = existingQuantity + 1;

                existingItem.Quantity = newQuantity; // Update quantity

                // Update ItemIdQuantity
                existingItem.ItemIdQuantity = itemId + '-' + newQuantity.ToString();                                                                // existingItem.ItemIdQuantity = itemId + '-' + quantity + 1;
                cartItems.Add(existingItem);

            }

            // Serialize and store the updated cart items in the session
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cartItems));

            // Return the response
            return Json(new { newItem = existingItem });
        }

        public JsonResult RemoveFromCart([FromBody] int itemId)
        {
            string cartSession = HttpContext.Session.GetString("Cart");
            List<CartVM> cartItems;

            if (cartSession != null)
            {
                cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartVM>>(cartSession);
            }
            else
            {
                cartItems = new List<CartVM>();
            }

            // Check if the item is already in the cart
            var existingItem = cartItems.FirstOrDefault(c => c.ItemId == itemId);
            if (existingItem != null)
            {
                // Remove the item from the cart
                cartItems.Remove(existingItem);
            }

            // Serialize and store the updated cart items in the session
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cartItems));

            // Return the response
            return Json(new { success = true, message = "Item removed from the cart." });
        }

    }
}
