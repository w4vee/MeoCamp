using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IConfiguration configuration, IProductService productService, ILogger<ProductController> logger)
        {
            _configuration = configuration;
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("add-new-product")]
        public async Task<IActionResult> AddNewProdcut(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddNewProduct(product.ProductName, product.Description, product.Price, product.RentalPrice, product.IsRentable, product.CategoryId, product.Status, product.Image);

                if (result != null)
                {
                    return Ok("Product added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add product.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdcut(int id, UpdateProductModel product)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.UpdateProduct(id, product);

                if (result)
                {
                    return Ok("Product update successfully.");
                }
                else
                {
                    return BadRequest("Failed to update product.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var isDeleted = await _productService.SoftDeleteProduct(id);

                if (isDeleted > 0)
                {
                    return Ok("Product deleted successfully.");
                }
                else
                {
                    return Ok("Product deleted fail.");
                }
            }
            catch (Exception ex)
            {
                return Ok("Fail");
            }
        }
    }
}
