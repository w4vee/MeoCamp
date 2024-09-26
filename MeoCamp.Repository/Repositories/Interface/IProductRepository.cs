using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeoCamp.Data.Repositories.Interface
{
    public interface IProductRepository
    {
        public Task<Product> AddNewProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task<int> SoftDeleteProduct(int id);
        public Task<Product> GetProductById(int id);
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<double?> GetPriceById(int id);
    }
}
