using Sips.Data;
using Sips.SipsModels;
using Sips.ViewModels;

namespace Sips.Repositories
{
    public class TransactionRepo
    {
        private readonly SipsdatabaseContext _db;

        public TransactionRepo(SipsdatabaseContext db)
        {
            _db = db;
        }
        public List<PayPalVM> GetTransactions()
        {
            SipsdatabaseContext db = _db;
            List<PayPalVM> transactions = _db.PayPalVMs
                    .Select(transaction => new PayPalVM
                    {
                        TransactionId = transaction.TransactionId,
                        Amount = transaction.Amount,
                        PayerName = transaction.PayerName,
                        PayerEmail = transaction.PayerEmail,
                        CreatedDate = transaction.CreatedDate,
                        PaymentMethod = transaction.PaymentMethod,
                        Currency = transaction.Currency
                    })
                    .ToList();
            return transactions;
        }
        public List<CheckoutVM> GetCheckout()
        {
            SipsdatabaseContext db = _db;
            List<CheckoutVM> checkouts = _db.CheckoutVMs
                    .Select(checkout => new CheckoutVM
                    {
                        ItemId = checkout.ItemId,
                        Name = checkout.Name,
                        BasePrice = checkout.BasePrice,
                        Quantity = checkout.Quantity,
                        AddInName = checkout.AddInName,
                        AddInPriceModifier = checkout.AddInPriceModifier,
                        SweetnessPercent = checkout.SweetnessPercent,
                        IcePercent = checkout.IcePercent,
                        Subtotal = checkout.Subtotal,
                        MilkType = checkout.MilkType,
                        MilkPriceModifier = checkout.MilkPriceModifier,
                        AddInNames = checkout.AddInNames,
                        AddInPriceModifiers = checkout.AddInPriceModifiers
                    })
                    .ToList();
            return checkouts;
        }
    }


}
