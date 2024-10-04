using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
        public Task<Category> GetCategoryById(int id);
        public Task<Category> GetCategoryByName(string name);
        public Task<List<Category>> GetAllCategories();
    }
}
