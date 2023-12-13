using System.Reflection;

namespace Sips.Models
{
    public class MilkTeaViewModel
    {
        public string title =  "Milk Tea";
        public Item Item { get; set; }

        public List<Item> Items { get; set; }
    }
}
