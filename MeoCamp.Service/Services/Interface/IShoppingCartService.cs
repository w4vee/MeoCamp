﻿using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
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
        public Task<ShoppingCart> GetShoppingCartByCustomerIdAsync(int customerId);
        public Task<bool> RemoveCartItem(CartItem cartItem);
        public Task<CartItem> GetCartItemById(int cartItemId);
        public Task<bool> UpdateItemQuantity(int id, UpdateItemModel item);
    }
};
