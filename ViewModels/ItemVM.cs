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

        public decimal BasePrice { get; set; }

        public int Inventory { get; set; }

        public string? UrlString { get; set; }
        public ItemType? ItemType  { get; set; }
    }
}
