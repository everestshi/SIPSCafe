using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sips.SipsModels;
using Sips.ViewModels;

namespace Sips.Repositories
{
    public class ProductRepository
    {
        private readonly SipsdatabaseContext _db;

        public ProductRepository(SipsdatabaseContext db)
        {
            _db = db;
        }

        public IEnumerable<ItemVM> GetAll()
        {
            var products = _db.Items.ToList();
            List<ItemVM> itemsVM = new List<ItemVM>();
            foreach (var p in products)
            {
                ItemVM itemVM = new ItemVM()
                {
                    ItemId = p.ItemId,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    Inventory = p.Inventory,
                    UrlString = p.UrlString,
                    ItemType = p.ItemType,

                };

                itemsVM.Add(itemVM);
            }

            return itemsVM;
        }

        public List<SelectListItem> GetItemTypes()
        {
            var itemTypes = _db.ItemTypes
                .Select(t => t.ItemTypeName)
                .Distinct()
                .Select(type => new SelectListItem
                {
                    Value = type,
                    Text = type
                }).ToList();
            return itemTypes;
        }

        
        public ItemVM GetById(int id)
        {
            var p = _db.Items.FirstOrDefault(p => p.ItemId == id);
            var itemVM = new ItemVM
            {
                ItemId = p.ItemId,
                Name = p.Name,
                Description = p.Description,
                BasePrice = p.BasePrice,
                Inventory = p.Inventory,
                UrlString = p.UrlString,
                ItemType = p.ItemType,
            };

            return itemVM;
        }

        public string Add(ItemVM proVM)
        {
            string message = string.Empty;
            Item item = new Item
            {
                Name = proVM.Name,
                Description = proVM.Description,
                BasePrice = proVM.BasePrice,
                Inventory = proVM.Inventory,
                UrlString = proVM.UrlString,
                ItemType = proVM.ItemType,
            };
            try
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                message = $"Product {item.Name} added successfully";
            }
            catch (Exception e)
            {
                message = $" Error saving product {item.Name}: {e.Message}";
            }
            return message;
        }

        //public string Update(Item editingItem)
        //{
        //    string message = string.Empty;
        //    try
        //    {
        //        Item item = GetById(editingItem.ItemId);
        //        item.Sweetness = editingItem.Sweetness;
        //        item.Description = editingItem.Description;
        //        item.Ice = editingItem.Ice;
        //        item.Name = editingItem.Name;
        //        item.ItemType = editingItem.ItemType;
        //        item.BasePrice = editingItem.BasePrice;
        //        item.Inventory = editingItem.Inventory;
        //        //item.urlString = editingItem.urlString;
                
        //        _db.SaveChanges();
        //        message = $"Product {editingItem.Name} updated successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        message = $" Error updating Product {editingItem.Name} : {e.Message}";
        //    }
        //    return message;
        //}

        //public string Delete(int id)
        //{
        //    string message = string.Empty;
        //    try
        //    {
        //        Item item = GetById(id);
        //        _db.Items.Remove(item);
        //        _db.SaveChanges();
        //        message = $"{item.Name} deleted successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        message = $" Error deleting product-{id}: {e.Message}";
        //    }
        //    return message;
        //}

    }
}
