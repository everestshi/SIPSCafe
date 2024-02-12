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
            List<Item> items = _db.Items.Where(item => item.ItemTypeId == 1).ToList();

            return items;
        }

        public IEnumerable<Item> GetFruitTeas()
        {
            List<Item> items = _db.Items.Where(item => item.ItemTypeId == 2).ToList();

            return items;
        }

        public IEnumerable<Item> GetSlushes()
        {
            List<Item> items = _db.Items.Where(item => item.ItemTypeId == 3).ToList();

            return items;
        }
    }
}
