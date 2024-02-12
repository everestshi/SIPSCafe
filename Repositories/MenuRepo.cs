using Sips.Data;
using Sips.SipsModels;

namespace Sips.Repositories
{
    public class MenuRepo
    {
        private readonly SipsdatabaseContext _db;



        public MenuRepo(SipsdatabaseContext db)
        {
            _db = db;
        }

        public IEnumerable<Item> GetMilkTeas()
        {
            List<Item> items = _db.Items.Where(item => item.ItemType == "Milk Tea").ToList();

            return items;
        }

        public IEnumerable<Item> GetFruitTeas()
        {
            List<Item> items = _db.Items.Where(item => item.ItemType == "Fruit Tea").ToList();

            return items;
        }

        public IEnumerable<Item> GetSlushes()
        {
            List<Item> items = _db.Items.Where(item => item.ItemType == "Slush").ToList();

            return items;
        }
    }
}
