using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using Microsoft.EntityFrameworkCore;
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
        private readonly GenericRepository<Payment> _genericRepoPayment;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly MeoCampDBContext _context;

        public OrderService(GenericRepository<Order> genericRepo, IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IProductRepository productRepository, GenericRepository<Payment> genericRepoPayment, MeoCampDBContext context)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _genericRepo = genericRepo;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _genericRepoPayment = genericRepoPayment;
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderDetailByIdAsync(id);
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

        public async Task<bool> Checkout(int customerId, string paymentMethod, int amount)
        {
            // Tạo đơn hàng từ giỏ hàng
            var cart = await _shoppingCartRepository.GetCartByUserId(customerId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Giỏ hàng không tồn tại hoặc trống.");
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = (int?)cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                OrderStatus = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                OrderDetails = new List<OrderDetail>()

            };

            foreach (var item in cart.CartItems)
            {
                var price = await _productRepository.GetPriceById(item.ProductId); // Lấy giá sản phẩm
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = (int?)item.Product.Price,
                    TotalPrice = ((int?)(item.Quantity * item.Product.Price)),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }
            await _orderRepository.CreateOrder(order); 

            // Tạo thông tin thanh toán
            var payment = new Payment
            {
                OrderId = order.Id,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentMethod,
                Amount = amount,
                PaymentStatus = "Success" 
            };

            await _genericRepoPayment.CreateAsync(payment); 

            
            //await _shoppingCartRepository.ClearCart(cart);
            await _shoppingCartRepository.RemoveAllCartItems(cart);
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


        public async Task<bool> UpdateOrder(int id, UpdateOrderModel order)
        {
            try
            {
                var existingOrder = await _genericRepo.GetByIdAsync(id);
                if (existingOrder == null)
                {
                    return false;
                }
                existingOrder.OrderStatus = order.OrderStatus;
                existingOrder.UpdatedAt = DateTime.Now ;
                
                await _genericRepo.UpdateAsync(existingOrder);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error");
                return false;
            }
        }
    }
}
