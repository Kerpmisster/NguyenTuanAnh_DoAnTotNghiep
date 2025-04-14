using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Category;
using QLNT_API.Services.CategoryServices;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO dto)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var created = await _categoryService.CreateAsync(dto, wwwRootPath);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("{parentId}/children")]
        public async Task<IActionResult> AddChildCategory(int parentId, [FromForm] CreateCategoryDTO dto)
        {
            try
            {
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var result = await _categoryService.AddChildCategoryAsync(parentId, dto, wwwRootPath);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO dto)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                await _categoryService.UpdateAsync(id, dto, wwwRootPath);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("force/{id}")]
        public async Task<IActionResult> ForceDelete(int id)
        {
            var success = await _categoryService.ForceDeleteAsync(id);
            if (!success)
                return NotFound("Không tìm thấy danh mục để xóa thật.");
            return Ok("Đã xóa vĩnh viễn khỏi cơ sở dữ liệu.");
        }
    }
}
