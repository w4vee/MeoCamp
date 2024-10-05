using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly MeoCampDBContext _context;

        public ContactRepository(MeoCampDBContext context)
        {
            _context = context;
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Contact>> GetAllContactAsync()
        {
            return await _context.Set<Contact>().ToListAsync();
        }

        public async Task<Contact> GetContactbyIdAsync(int Id)
        {
            var contact = await _context.Contacts.FindAsync(Id);
            return contact;
        }

        public async Task<List<Contact>> GetContactbyUsernameAsync(string user_name)
        {
            var contacts = await _context.Contacts.Where(x => x.User_name == user_name).ToListAsync();
            return contacts;
        }
    }
}
