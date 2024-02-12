using Sips.Data;
using Sips.SipsModels;

namespace Sips.Repositories
{
    public class ContactRepo
    {
        private readonly SipsdatabaseContext _db;

        public ContactRepo(SipsdatabaseContext db)
        {
            this._db = db;
        }

        public void RegisterUser(Contact contact)
        {
            
            _db.Contacts.Add(contact);
            _db.SaveChanges();

        }
        public Contact GetRegisteredUser(string email)
        {
            var user = _db.Contacts.FirstOrDefault(u => u.Email == email);
            return user;

        }

        public string GetFullNameByEmail(string email)
        {
            var user = _db.Contacts.FirstOrDefault(u => u.Email == email);
            return user != null ? $"{user.FirstName} {user.LastName}" : null;
        }
    }
}
