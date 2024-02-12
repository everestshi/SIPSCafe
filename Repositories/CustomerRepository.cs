using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sips.SipsModels;

namespace Sips.Repositories
{
    public class CustomerRepository
    {
        private readonly SipsdatabaseContext _db;

        public CustomerRepository(SipsdatabaseContext db)
        {
            _db = db;
        }
        public IEnumerable<Contact> GetAll()
        {
            return _db.Contacts;
        }
        public Contact GetById(int id)
        {
            return _db.Contacts.FirstOrDefault(p => p.UserId == id);
        }

        public string Add(Contact contact)
        {
            string message = string.Empty;
            try
            {
                _db.Add(contact);
                _db.SaveChanges();
                message = $"Customer {contact.Email} added successfully";
            }
            catch (Exception e)
            {
                message = $" Error saving customer {contact.Email}: {e.Message}";
            }
            return message;
        }

        public string Update(Contact edittingcontact)
        {
            string message = string.Empty;
            try
            {
                Contact contact = GetById(edittingcontact.UserId);
                contact.Email = edittingcontact.Email;
                contact.FirstName = edittingcontact.FirstName;
                contact.LastName = edittingcontact.LastName;
                contact.PhoneNumber = edittingcontact.PhoneNumber;
                contact.City = edittingcontact.City;
                contact.Street = edittingcontact.Street;
                contact.Province = edittingcontact.Province;
                contact.Unit = edittingcontact.Unit;
                contact.PostalCode = edittingcontact.PostalCode;

                _db.SaveChanges();
                message = $"Customer {edittingcontact.Email} updated successfully";
            }
            catch (Exception e)
            {
                message = $" Error updating customer {edittingcontact.Email} : {e.Message}";
            }
            return message;
        }

        public string Delete(Contact contact)
        {
            string message = string.Empty;
            try
            {
                //Contact contact = GetById(id);
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                message = $"{contact.Email} deleted successfully";
            }
            catch (Exception e)
            {
                message = $" Error deleting customer: {e.Message}";
            }
            return message;
        }
    }
}
