using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IProductService
    {
        public Task<Product> AddNewProduct(string name, string description, double? price, double? rentalprice, bool? isrenable, int? categoryId, bool? status, List<string> images, int quantity, double rate);

        public Task<IEnumerable<Product>> GetAllProductsAsync();

        public Task<bool> UpdateProduct(int id, UpdateProductModel model);

        public Task<int> SoftDeleteProduct(int id);

        public Task<Product> GetProductById(int id);
    }
}