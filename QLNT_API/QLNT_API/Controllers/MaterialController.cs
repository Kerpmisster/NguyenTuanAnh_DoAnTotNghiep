using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Extension;
using QLNT_API.DTO.Material;
using QLNT_API.Services.ExtensionServices;
using QLNT_API.Services.MaterialServices;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IProductService _productService;

        public MaterialController(IMaterialService materialService, IProductService productService)
        {
            _materialService = materialService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaterialDTO>>> GetAll()
        {
            var extensions = await _materialService.GetAllAsync();
            return Ok(extensions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDTO>> GetById(int id)
        {
            try
            {
                var extension = await _materialService.GetByIdAsync(id);
                return Ok(extension);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MaterialDTO>> Create([FromBody] CreateMaterialDTO dto)
        {
            // ✅ Kiểm tra ProductId có tồn tại không
            if (!await _productService.ProductExists(dto.ProductId))
            {
                return BadRequest(new { message = "ProductId không tồn tại." });
            }

            try
            {
                var result = await _materialService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMaterialDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "Mismatched Extension ID." });

            try
            {
                var success = await _materialService.UpdateAsync(dto);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _materialService.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
