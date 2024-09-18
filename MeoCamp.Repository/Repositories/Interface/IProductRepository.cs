using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface{
    public interface IProductRepository{
        public Task<Product?> GetProductByProductnameAsync(string productname);
    }
}