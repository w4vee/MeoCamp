using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

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
            var imagesList = product.Images.ToList();
            if (ModelState.IsValid)
            {
                var result = await _productService.AddNewProduct(product.ProductName, product.Description, product.Price, product.RentalPrice, product.IsRentable, product.CategoryId, product.Status, imagesList, product.Quantity, product.Rate);

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
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            // Tạo danh sách DTO cho các sản phẩm
            var productDtos = products.Select(product => new
            {
                product.Id,
                product.ProductName,
                product.Description,
                product.Price,
                product.RentalPrice,
                product.IsRentable,
                product.CategoryId,
                product.CreatedAt,
                product.UpdatedAt,
                product.Status,
                Images = product.Image, // Trả về trực tiếp danh sách hình ảnh
                product.Quantity,
                product.Rate,
                product.CartItems,
                product.Category,
                product.OrderDetails,
                product.Rentals
            }).ToList();

            return Ok(productDtos);
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

           
            var productDto = new
            {
                product.Id,
                product.ProductName,
                product.Description,
                product.Price,
                product.RentalPrice,
                product.IsRentable,
                product.CategoryId,
                product.CreatedAt,
                product.UpdatedAt,
                product.Status,
                Images = product.Image, 
                product.Quantity,
                product.Rate,
                product.CartItems,
                product.Category,
                product.OrderDetails,
                product.Rentals
            };

            return Ok(productDto);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    var product = await _productService.GetProductById(id);

        //    if (product == null)
        //    {
        //        return NotFound("Sản phẩm không tồn tại.");
        //    }

        //    var imageUrls = JsonConvert.DeserializeObject<List<string>>(product.Image);

        //    var productDto = new
        //    {
        //        product.Id,
        //        product.ProductName,
        //        product.Description,
        //        product.Price,
        //        product.RentalPrice,
        //        product.IsRentable,
        //        product.CategoryId,
        //        product.Status,
        //        Images = imageUrls ?? new List<string>(), // Trả về danh sách các đường dẫn hình ảnh
        //        product.Quantity,
        //        product.Rate,
        //        product.CreatedAt,
        //        product.UpdatedAt
        //    }; 

        //    return Ok(product);
        //}
    }
}
