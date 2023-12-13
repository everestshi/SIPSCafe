using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sips.Models;

namespace Sips.Repositories
{
    public class CustomerRepository
    {
        private readonly SipsContext _db;

        public CustomerRepository(SipsContext db)
        {
            _db = db;
        }
        public IEnumerable<Contact> GetAll()
        {
            return _db.Contacts;
        }
        public Contact GetById(int id)
        {
            return _db.Contacts.FirstOrDefault(p => p.PkUserId == id);
        }
    }
}
