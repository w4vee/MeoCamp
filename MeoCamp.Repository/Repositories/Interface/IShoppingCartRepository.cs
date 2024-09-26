using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IShoppingCartRepository
    {
        public Task<ShoppingCart> GetCartByUserId(int id);

        public Task<ShoppingCart> UpdateCart(ShoppingCart cart);

        public Task<ShoppingCart> ClearCart(ShoppingCart cart);
    }
}
