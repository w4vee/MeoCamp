using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IOrderService
    {
        public Task<bool> AddNewOrder(CreateOrderModel model);

        public Task<IEnumerable<Order>> GetAllOrdersAsync();

        public Task<Order> GetOrderById(int id);
    }
}
