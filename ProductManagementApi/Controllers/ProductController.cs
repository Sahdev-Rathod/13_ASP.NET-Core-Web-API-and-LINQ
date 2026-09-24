using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Interfaces;
using ProductViewModel.DTOUiModel;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductUi product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUi product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(id, product);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAndSortProducts( string? category,  string? name, decimal? minPrice, decimal? maxPrice,string? sort)
            {
            // Get all products
            var products = await _productService.GetAllProductsAsync();

            // Filter by Category
            if (!string.IsNullOrEmpty(category))
            {
                products = products
                    .Where(x => x.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filter by Name
            if (!string.IsNullOrEmpty(name))
            {
                products = products
                    .Where(x => x.Name.Contains(
                        name,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filter by Minimum Price
            if (minPrice.HasValue)
            {
                products = products
                    .Where(x => x.Price >= minPrice.Value)
                    .ToList();
            }

            // Filter by Maximum Price
            if (maxPrice.HasValue)
            {
                products = products
                    .Where(x => x.Price <= maxPrice.Value)
                    .ToList();
            }

            // Sort by Price
            if (sort?.ToLower() == "asc")
            {
                products = products
                    .OrderBy(x => x.Price)
                    .ToList();
            }
            else if (sort?.ToLower() == "desc")
            {
                products = products
                    .OrderByDescending(x => x.Price)
                    .ToList();
            }

            return Ok(products);
        }
    }
}
