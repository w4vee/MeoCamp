using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeoCamp.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MeoCampDBContext _context;

        public ProductRepository(MeoCampDBContext context)
        {
            _context = context;
        }
        public async Task<Product> AddNewProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<double?> GetPriceById(int id)
        {
            var price = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => p.Price) // Lấy giá
                .FirstOrDefaultAsync();

            return price; // Trả về giá
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SoftDeleteProduct(int id)
        {
            try
            {
                var productSoftDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (productSoftDelete != null)
                {
                    productSoftDelete.Status = false;
                    var result = await _context.SaveChangesAsync();
                    return result;
                }
                return -1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.RentalPrice = product.RentalPrice;
                existingProduct.IsRentable = product.IsRentable;
                existingProduct.UpdatedAt = product.UpdatedAt;
                existingProduct.Status = product.Status;
                existingProduct.Image = product.Image;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Rate = product.Rate;

                await _context.SaveChangesAsync();
            }
            return existingProduct;
        }
    }
}
