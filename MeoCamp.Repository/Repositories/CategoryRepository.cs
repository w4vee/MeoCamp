using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MeoCampDBContext _context;

        public CategoryRepository(MeoCampDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync(); ;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == name);
            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var tracker = _context.Attach(category);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
