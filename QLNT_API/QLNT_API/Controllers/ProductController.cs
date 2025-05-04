using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Product;
using QLNT_API.Services.CategoryServices;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO dto)
        {
            // Kiểm tra Category tồn tại trước khi thêm Product
            if (!dto.Cid.HasValue || !await _categoryService.CategoryExists(dto.Cid.Value))
            {
                return BadRequest("Invalid Category ID.");
            }

            var wwwRootPath = _env.WebRootPath;
            var createdProduct = await _productService.CreateAsync(dto, wwwRootPath);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDTO dto)
        {
            try
            {
                var result = await _productService.UpdateAsync(id, dto, _env.WebRootPath);
                return result ? Ok("Cập nhật thành công") : BadRequest("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FilterProducts([FromBody] FilterDTO filter)
        {
            var products = await _productService.GetProductsAsync(filter);
            return Ok(products);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
