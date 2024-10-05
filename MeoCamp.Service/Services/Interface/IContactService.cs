using MeoCamp.Data.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IContactService
    {
        public Task<List<Contact>> GetAllContactAsync();
        public Task<List<Contact>> GetContactbyUsernameAsync(string name);
        public Task<Contact> CreateContact(ContactModel model);
        public Task<bool> DeleteContact(int Id);
    }
}
