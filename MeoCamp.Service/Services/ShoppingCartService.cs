﻿using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly GenericRepository<ShoppingCart> _genericRepo;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, GenericRepository<ShoppingCart> genericRepo)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _genericRepo = genericRepo;
        }

        public async Task<bool> AddToCart(int customerId, int productId, int quantity)
        {
            // Lấy giỏ hàng theo customerId
            var cart = await _shoppingCartRepository.GetCartByUserId(customerId);

            // Nếu giỏ hàng không tồn tại, tạo mới
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    CustomerId = customerId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CartItems = new List<CartItem>() // Khởi tạo danh sách CartItems
                };
                await _genericRepo.CreateAsync(cart);
            }

            // Tìm mục sản phẩm trong giỏ hàng
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                // Nếu đã tồn tại, cập nhật số lượng
                existingCartItem.Quantity += quantity;
                existingCartItem.AddedAt = DateTime.Now;
            }
            else
            {
                // Nếu chưa tồn tại, thêm mới mục vào giỏ hàng
                var cartItem = new CartItem
                {
                    CartId = cart.Id, // Đảm bảo CartId đã được gán chính xác
                    ProductId = productId,
                    Quantity = quantity,
                    AddedAt = DateTime.Now
                };
                cart.CartItems.Add(cartItem);
            }

            // Cập nhật giỏ hàng
            await _shoppingCartRepository.UpdateCart(cart);
            return true;
        }

    }
}
