using MeoCamp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IContactRepository
    {
        public Task<List<Contact>> GetAllContactAsync();
        public Task<List<Contact>> GetContactbyUsernameAsync(string user_name);
        public Task<Contact> GetContactbyIdAsync(int Id);
        public Task<Contact> CreateContact(Contact contact);
        public Task<bool> DeleteContact(Contact contact);
    }
}
