using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        GenericRepository<Contact> _genericRepo;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
            _genericRepo ??= new GenericRepository<Contact>();
        }

        public async Task<Contact> CreateContact(ContactModel model)
        {
            var contact = new Contact
            {
                User_name = model.User_name,
                Mail = model.Mail,
                Phone = model.Phone,
                Description = model.Description,
            };

            await _contactRepository.CreateContact(contact);

            return contact;
           
        }

        public async Task<bool> DeleteContact(int Id)
        {
            var contact = await _contactRepository.GetContactbyIdAsync(Id);
            if (contact != null)
            {
                await _contactRepository.DeleteContact(contact);
                return true;
            }
            return false;
        }

        public async Task<List<Contact>> GetAllContactAsync()
        {
            try
            {
                var List = await _contactRepository.GetAllContactAsync();
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception("Loi lay list");
            }
        }

        public async Task<List<Contact>> GetContactbyUsernameAsync(string name)
        {
            try
            {
                var List = await _contactRepository.GetContactbyUsernameAsync(name);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception("Loi lay list");
            }
        }
    }
}
