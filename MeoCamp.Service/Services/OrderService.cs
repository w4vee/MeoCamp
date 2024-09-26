using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
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
        private readonly IOrderRepository _orderRepository;
        private readonly GenericRepository<Order> _genericRepo;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderService(GenericRepository<Order> genericRepo, IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _genericRepo = genericRepo;
            _orderRepository = orderRepository;
        }

        public async Task<bool> ProcessPayment(int customerId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserId(customerId);

            if (cart == null || !cart.CartItems.Any())
            {
                return false;
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = (int?)cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                OrderStatus = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            foreach (var cartItem in cart.CartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = (int?)cartItem.Product.Price,
                    TotalPrice = ((int?)(cartItem.Quantity * cartItem.Product.Price)),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                order.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.CreateOrder(order);
            await _shoppingCartRepository.ClearCart(cart);
            return true;
        }


        public async Task<bool> AddNewOrder(CreateOrderModel model)
        {
            Order order = new Order
            {
                CustomerId = model.CustomerId,
                OrderDate = model.OrderDate,
                TotalAmount = (int?)model.TotalAmount,
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
