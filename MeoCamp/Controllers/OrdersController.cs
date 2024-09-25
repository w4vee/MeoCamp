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

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
