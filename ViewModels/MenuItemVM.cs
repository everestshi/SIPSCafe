using System.Reflection;
using Sips.SipsModels;

namespace Sips.ViewModels
{
    public class MenuItemVM
    {
        public string Title { get; set; } = "Milk Tea";
        public List<Item> Items { get; set; }
        public List<string> IceOptions { get; set; } // List of ice options
        public List<string> SweetnessOptions { get; set; } // List of sweetness options
        public List<string> MilkOptions { get; set; } // List of milk options
        public List<string> AddonOptions { get; set; } // List of addon options

        // Constructor to initialize lists
        public MenuItemVM()
        {
            IceOptions = new List<string>();
            SweetnessOptions = new List<string>();
            MilkOptions = new List<string>();
            AddonOptions = new List<string>();
        }
    }
}
