using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Extension;
using QLNT_API.Services.ExtensionServices;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly IExtensionService _extensionService;
        private readonly IProductService _productService;

        public ExtensionController(IExtensionService extensionService, IProductService productService)
        {
            _extensionService = extensionService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExtensionDTO>>> GetAll()
        {
            var extensions = await _extensionService.GetAllAsync();
            return Ok(extensions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExtensionDTO>> GetById(int id)
        {
            try
            {
                var extension = await _extensionService.GetByIdAsync(id);
                return Ok(extension);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExtensionDTO>> Create([FromBody] CreateExtensionDTO dto)
        {
            // ✅ Kiểm tra ProductId có tồn tại không
            if (!await _productService.ProductExists(dto.ProductId))
            {
                return BadRequest(new { message = "ProductId không tồn tại." });
            }

            try
            {
                var result = await _extensionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExtensionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "Mismatched Extension ID." });

            try
            {
                var success = await _extensionService.UpdateAsync(dto);
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
                var success = await _extensionService.DeleteAsync(id);
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
