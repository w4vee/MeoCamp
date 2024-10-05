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

        public Task<bool> ProcessPayment(int customerId);

        public Task<bool> Checkout(int customerId, string paymentMethod, int amount);

        public Task<Order> GetOrderByIdAsync(int id);

        public Task<bool> UpdateOrder(int id, UpdateOrderModel order);
        
    }
}
