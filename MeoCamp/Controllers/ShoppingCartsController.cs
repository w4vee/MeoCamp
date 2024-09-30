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

        public class AddToCartRequest
        {
            public int customerId { get; set; }
            public int productId { get; set; }
            public int quantity { get; set; }
        }
        
    }
}
