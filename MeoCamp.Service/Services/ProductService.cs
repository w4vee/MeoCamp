using MeoCamp.Data.Repositories;
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
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly GenericRepository<Product> _genericRepo;

        public ProductService(ProductRepository productRepository, GenericRepository<Product> genericRepo)
        {
            _productRepository = productRepository;
            _genericRepo = genericRepo;
        }

        public async Task<Product> AddNewProduct(string name, string description, double? price, double? rentalprice, bool? isrentable, int? categoryId, bool? status, string image, int quantity, double rate)
        {
            
            Product newProduct = new Product
            {
                ProductName = name,
                Description = description,
                Price = price,
                RentalPrice = rentalprice,
                IsRentable = isrentable,
                CategoryId = categoryId,
                Status = status,
                Image = image,
                Quantity = quantity,
                Rate = rate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

           await _productRepository.AddNewProduct(newProduct);

            return newProduct;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _genericRepo.GetAllAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return _genericRepo.GetByIdAsync(id);
        }

        public async Task<int> SoftDeleteProduct(int id)
        {
            var productSoftDelete = await _productRepository.SoftDeleteProduct(id);
            return productSoftDelete;
        }

        public async Task<bool> UpdateProduct(int id, UpdateProductModel product)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductById(id);
                if (existingProduct == null)
                {
                    return false;
                }
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.RentalPrice = product.RentalPrice;
                existingProduct.IsRentable = product.IsRentable;
                existingProduct.UpdatedAt = DateTime.Now;
                existingProduct.Status = product.Status;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Rate = product.Rate;
                await _productRepository.UpdateProduct(existingProduct);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error");
                return false;
            }
        }
    }
}
