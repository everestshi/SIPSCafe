using System.ComponentModel.DataAnnotations;

namespace Sips.SipsModels.ViewModels
{
    public class PayPalVM
    {
        [Key]
        [Display(Name = "Payment ID")]
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        [Display(Name = "Name")]
        public string PayerName { get; set; }
        [Display(Name = "Email")]
        public string PayerEmail { get; set; }
        [Display(Name = "Created")]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public string CreatedDate { get; set; }
        [Display(Name = "MOP")]
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
    }
}
