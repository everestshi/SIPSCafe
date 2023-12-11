using Sips.Models;

namespace Sips.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Item> GetMilkTeas();
        IEnumerable<Item> GetFruitTeas();
        IEnumerable<Item> GetSlushes();
    }
}
