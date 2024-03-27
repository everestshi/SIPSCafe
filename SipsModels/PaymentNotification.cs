using System.ComponentModel.DataAnnotations;

namespace Sips.SipsModels
{
    public class PaymentNotification
    {
        [Key]
        [Display(Name = "Payment Id:")]
        public string PaymentID { get; set; }

        [Display(Name = "Amount Paid:")]
        public string Amount { get; set; }

        [Display(Name = "Currency Code:")]
        public string CurrencyCode { get; set; }

        [Display(Name = "Currency Symbol:")]
        public string CurrencySymbol { get; set; }

        [Display(Name = "Payer Id:")]
        public string PayerId { get; set; }

        [Display(Name = "Payer Name:")]
        public string PayerFullName { get; set; }

        [Display(Name = "Capture Id:")]
        public string CaptureId { get; set; }
    }

}
