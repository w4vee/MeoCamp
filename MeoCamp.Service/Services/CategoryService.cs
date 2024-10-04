using MeoCamp.Data.Repositories;
using MeoCamp.Repository.Models;
using MeoCamp.Repository;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeoCamp.Data.Repositories.Interface;

namespace MeoCamp.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly GenericRepository<Category> _genericRepo;

        public CategoryService(ICategoryRepository categoryRepository, GenericRepository<Category> genericRepo)
        {
            _categoryRepository = categoryRepository;
            _genericRepo = genericRepo;
        }
        public async Task<Category> CreateCategory(CategoryModel model)
        {
            var existingCategory = await _categoryRepository.GetCategoryByName(model.CategoryName);
            if (existingCategory != null)
            {
                // throw new KeyNotFoundException("Người dùng này đã tồn tại");
                return null;
            }

            var newCategory = new Category
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
            };

            await _categoryRepository.CreateCategory(newCategory);

            return newCategory;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var listCategory = await _categoryRepository.GetAllCategories();
            return listCategory;
        }

        public async Task<Category> UpdateCategory(int id, CategoryModel model)
        {
            var existingCategory = await _categoryRepository.GetCategoryById(id);
            if (existingCategory == null)
            {
                // throw new KeyNotFoundException("Người dùng ko đã tồn tại");
                return null;
            }
            if (model.CategoryName != null && model.CategoryName != " ")
            {
                existingCategory.CategoryName = model.CategoryName;
            }
            else if (model.CategoryName != null)
            {
                existingCategory.Description = model.Description;
            }

            await _categoryRepository.UpdateCategory(existingCategory);

            return existingCategory;
        }
    }
}
