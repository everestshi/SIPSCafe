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
                Contact contact = GetById(edittingcontact.PkUserId);
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

        public string Delete(int id)
        {
            string message = string.Empty;
            try
            {
                Contact contact = GetById(id);
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                message = $"{contact.Email} deleted successfully";
            }
            catch (Exception e)
            {
                message = $" Error deleting customer-{id}: {e.Message}";
            }
            return message;
        }
    }
}
