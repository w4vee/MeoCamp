using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeoCamp.Repository.Models;
using MeoCamp.Service.Services.Interface;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;

namespace MeoCamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private MeoCampDBContext _context;

        public OrdersController(IOrderService orderService, MeoCampDBContext context)
        {
            _orderService = orderService;
            _context = context;
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest checkoutReq)
        {
            bool result = await _orderService.Checkout(checkoutReq.customerId, checkoutReq.paymentMethod, checkoutReq.amount);

            if (result)
            {
                return Ok("Thanh toán thành công.");
            }
            return BadRequest("Không thể thanh toán.");
        }

        public class CheckoutRequest
        {
            public int customerId { get; set; }
            public string paymentMethod { get; set; }
            public int amount { get; set; }
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("get-detail/{id}")]
        public async Task<ActionResult<Order>> GetOrderDetail(int id)
        {
            // Gọi service để lấy dữ liệu
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        [HttpPost("add-new-order")]
        public async Task<IActionResult> AddNewOrder(CreateOrderModel order)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.AddNewOrder(order);

                if (result != null)
                {
                    return Ok("Order added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add order.");
                }
            }
            return BadRequest("Invalid data.");
        }


    }
}
