using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface ICategoryService
    {
        public Task<Category> CreateCategory(CategoryModel model);

        public Task<List<Category>> GetAllCategory();

        public Task<Category> UpdateCategory(int id, CategoryModel model);
    }
}
