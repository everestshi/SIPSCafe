using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sips.SipsModels;
using Sips.ViewModels;

namespace Sips.Controllers
{
    public class TransactionController : Controller
    {
        private readonly SipsdatabaseContext _db;
        private PayPalVM payPalVM;

        public IActionResult Index()
        {
            DbSet<OrderDetail> items = _db.OrderDetails;

            return View(items);
        }
        public IActionResult PayPal(PayPalVM payPalVM)
        {
            _db.PayPalVM.Add(payPalVM);
            _db.SaveChanges();
            return View(payPalVM);
        }
        public IActionResult Checkout()
        {             return View();
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

    }
}
