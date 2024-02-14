using Sips.SipsModels;
using System.ComponentModel.DataAnnotations;

namespace Sips.ViewModels
{
    public class ProductVM
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal BasePrice { get; set; }

        public int Inventory { get; set; }

        public string? UrlString { get; set; }

        //public int? SweetnessId { get; set; }

        //public int? IceId { get; set; }

        //public int? ItemTypeId { get; set; }

        public decimal? Ice { get; set; }

        public string? ItemType  { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public decimal? Sweetness  { get; set; }
    }
}
