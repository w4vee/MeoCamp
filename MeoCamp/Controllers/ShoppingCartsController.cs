using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeoCamp.Repository.Models;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using MeoCamp.Service.BusinessModel;

namespace MeoCamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        


        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, UpdateItemModel item)
        {
            if (ModelState.IsValid)
            {
                bool result = await _shoppingCartService.UpdateItemQuantity(id, item);

                if (result)
                {
                    return Ok("Quantity update successfully.");
                }
                else
                {
                    return BadRequest("Failed to update quantity.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest req)
        {
            bool result = await _shoppingCartService.AddToCart(req.customerId, req.productId, req.quantity);

            if (result)
            {
                return Ok("Sản phẩm đã được thêm vào giỏ hàng.");
            }
            return BadRequest("Không thể thêm sản phẩm vào giỏ hàng.");
        }


        [HttpGet("cart-items/{customerId}")]
        public async Task<IActionResult> GetCartItemsByCustomerId(int customerId)
        {
            // Lấy giỏ hàng của customer dựa trên CustomerId
            var shoppingCart = await _shoppingCartService.GetShoppingCartByCustomerIdAsync(customerId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found for this customer.");
            }

            // Lấy toàn bộ các CartItem từ giỏ hàng
            var cartItems = shoppingCart.CartItems.Select(cartItem => new
            {
                cartItem.ProductId,
                ProductName = cartItem.Product.ProductName,
                cartItem.Product.Price,
                cartItem.Product.RentalPrice,
                cartItem.Quantity,
                cartItem.AddedAt
            }).ToList();

            return Ok(cartItems);
        }

        [HttpDelete("remove-from-cart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _shoppingCartService.GetCartItemById(cartItemId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            var result = await _shoppingCartService.RemoveCartItem(cartItem);

            if (result)
            {
                return Ok("Item removed from cart successfully.");
            }

            return BadRequest("Failed to remove item from cart.");
        }

        

        public class AddToCartRequest
        {
            public int customerId { get; set; }
            public int productId { get; set; }
            public int quantity { get; set; }
        }
        
    }
}
