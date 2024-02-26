using Microsoft.AspNetCore.Mvc;
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
            var products = _db.Items.Include(p => p.ItemType).ToList();
            //var products = _db.Items.ToList();

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
                    ItemTypeId = p.ItemType?.ItemTypeId,
                    ItemTypeName = p.ItemType?.ItemTypeName,
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
            var p = _db.Items.Include(p => p.ItemType).FirstOrDefault(p => p.ItemId == id);
            var ItemTypeName = p.ItemType?.ItemTypeName;

            var itemVM = new ItemVM
            {
                ItemId = p.ItemId,
                Name = p.Name,
                Description = p.Description,
                BasePrice = p.BasePrice,
                Inventory = p.Inventory,
                ItemTypeId = p.ItemTypeId,
                ItemTypeName = p.ItemType?.ItemTypeName,

        };

            return itemVM;
        }

        public async Task<string> AddAsync(ItemVM proVM)
        {
            string message = string.Empty;
            IFormFile ImageFile = proVM.ImageFile;
            int imageID = await ImageSave(ImageFile);

            //if (imageStore != null)
            //{
            //   imageID = imageStore.ImageId;
            //}

            Item item = new Item
            {
                Name = proVM.Name,
                Description = proVM.Description,
                BasePrice = proVM.BasePrice,
                Inventory = proVM.Inventory,
                ItemTypeId = proVM.ItemTypeId,
                ImageId = imageID

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

        public string Update(ItemVM editingItem)
        {
            string message = string.Empty;
            Item item = _db.Items.Include(p => p.ItemType).FirstOrDefault(p => p.ItemId == editingItem.ItemId);

            try
            {
                item.Description = editingItem.Description;
                item.Name = editingItem.Name;
                item.ItemTypeId = editingItem.ItemTypeId;
                item.BasePrice = editingItem.BasePrice;
                item.Inventory = editingItem.Inventory;
                //item.urlString = editingItem.urlString;

                _db.SaveChanges();
                message = $"{editingItem.Name} updated successfully";
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
            Item item= _db.Items.Include(p => p.ItemType).FirstOrDefault(p => p.ItemId == id);

            try
            {
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


        public async Task<int> ImageSave(IFormFile ImageFile)
        {
            int  imageID = 0;
            string message = string.Empty;
            if (ImageFile != null && ImageFile.Length > 0)
                {
                    string contentType = ImageFile.ContentType;

                    if (contentType == "image/png" ||
                        contentType == "image/jpeg" ||
                        contentType == "image/jpg")
                    {
                        try
                        {
                            byte[] imageData;

                            using (var memoryStream = new MemoryStream())
                            {
                                ImageFile.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }

                            var image = new ImageStore
                            {
                                FileName = Path.
                                   GetFileNameWithoutExtension(ImageFile.FileName),
                                Image = imageData
                            };

                        _db.ImageStores.Add(image);
                        await _db.SaveChangesAsync();  
                        imageID = image.ImageId;

                    }
                    catch (Exception ex)
                        {
                        message = "An error occured uploading your image. Please try again.";
                        
                        }
                    }
                    else
                    {
                        message = "error, Please upload a PNG, JPG, or JPEG file.";
                    }
                }
                else
                {
                    message =  "Please select an  image to upload.";
                }
            

            return imageID;
        }

    }
}
