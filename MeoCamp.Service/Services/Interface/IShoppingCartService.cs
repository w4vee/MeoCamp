using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IShoppingCartService
    {
        public Task<bool> AddToCart(int customerId, int productId, int quantity);

    }
};
