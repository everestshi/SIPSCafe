using Sips.SipsModels;
using System.ComponentModel.DataAnnotations;

namespace Sips.ViewModels
{
    public class ItemVM
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        [Display(Name = "Base Price")]

        public decimal BasePrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Inventory cannot be negative.")]
        public int Inventory { get; set; }

        public string? UrlString { get; set; }

        [Display(Name = "Item Type")]
        [Required]
        public int? ItemTypeId { get; set; }

        [Display(Name = "Item Type")]
        [Required]
        public string? ItemTypeName { get; set; }


        //public ItemType? ItemType { get; set; }
    }
}
