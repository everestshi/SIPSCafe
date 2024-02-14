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
        //public IEnumerable<ProductVM> GetAll()
        //{
        //    return _db.ProductsVM;
        //}

        public IEnumerable<ProductVM> GetAll()
        {
            var products = _db.Items.ToList();
            List<ProductVM> productsVM = new List<ProductVM>();
            foreach (var p in products)
            {
                ProductVM productVm = new ProductVM()
                {
                    ItemId = p.ItemId,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    Inventory = p.Inventory,
                    UrlString = p.UrlString,
                    ItemType = p.ItemType != null ? p.ItemType.ItemTypeName : null,
                    Ice = p.Ice != null ? p.Ice.IcePercent : null,
                    Sweetness = p.Sweetness != null ? p.Sweetness.SweetnessPercent : null
                };

                productsVM.Add(productVm);
            }

            //_db.ProductsVM.AddRange(productsVM);
            //_db.SaveChanges();
            return productsVM;
        }
        public Item GetById(int id)
        {
            return _db.Items.FirstOrDefault(p => p.ItemId == id);
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
                Item item = GetById(editingItem.ItemId);
                item.Sweetness = editingItem.Sweetness;
                item.Description = editingItem.Description;
                item.Ice = editingItem.Ice;
                item.Name = editingItem.Name;
                item.ItemType = editingItem.ItemType;
                item.BasePrice = editingItem.BasePrice;
                item.Inventory = editingItem.Inventory;
                //item.urlString = editingItem.urlString;
                
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
