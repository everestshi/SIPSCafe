using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sips.Models;

namespace Sips.Repositories
{
    public class ProductRepository
    {
        private readonly SipsContext _db;

        public ProductRepository(SipsContext db)
        {
            _db = db;
        }
        public IEnumerable<Item> GetAll()
        {
            return _db.Items;
        }
        public Item GetById(int id)
        {
            return _db.Items.FirstOrDefault(p => p.PkItemId == id);
        }

        public string Add(Item item)
        {
            string message = string.Empty;
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

        public string Update(Item editingItem)
        {
            string message = string.Empty;
            try
            {
                Item item = GetById(editingItem.PkItemId);
                item.Sweetness = editingItem.Sweetness;
                item.Description = editingItem.Description;
                item.Ice = editingItem.Ice;
                item.Name = editingItem.Name;
                item.ItemType = editingItem.ItemType;
                item.BasePrice = editingItem.BasePrice;
                item.Inventory = editingItem.Inventory;
                
                _db.SaveChanges();
                message = $"Product {editingItem.Name} updated successfully";
            }
            catch (Exception e)
            {
                message = $" Error updating Product {editingItem.Name} : {e.Message}";
            }
            return message;
        }

        public string Delete(int id)
        {
            string message = string.Empty;
            try
            {
                Item item = GetById(id);
                _db.Items.Remove(item);
                _db.SaveChanges();
                message = $"{item.Name} deleted successfully";
            }
            catch (Exception e)
            {
                message = $" Error deleting product-{id}: {e.Message}";
            }
            return message;
        }

    }
}
