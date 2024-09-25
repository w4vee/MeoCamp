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
    public class OrderService : IOrderService
    {

        private readonly GenericRepository<Order> _genericRepo;

        public OrderService( GenericRepository<Order> genericRepo)
        {
           
            _genericRepo = genericRepo;
        }

        public async Task<bool> AddNewOrder(CreateOrderModel model)
        {
            Order order = new Order
            {
                CustomerId = model.CustomerId,
                OrderDate = model.OrderDate,
                TotalAmount = model.TotalAmount,
                OrderStatus = model.OrderStatus,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt

            };
            await _genericRepo.CreateAsync(order);
            return true;


        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _genericRepo.GetAllAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _genericRepo.GetByIdAsync(id);
        }

    }
}
