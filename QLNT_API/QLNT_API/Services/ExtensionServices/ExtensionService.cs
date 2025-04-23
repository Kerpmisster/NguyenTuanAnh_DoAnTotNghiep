using Microsoft.EntityFrameworkCore;
using QLNT_API.DTO.Extension;
using QLNT_API.Mapper;
using QLNT_API.Models;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Services.ExtensionServices
{
    public class ExtensionService : IExtensionService
    {
        private readonly QuanlynoithatContext _context;
        private readonly IProductService _productService;

        public ExtensionService(QuanlynoithatContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<ExtensionDTO> CreateAsync(CreateExtensionDTO dto)
        {
            var extension = await ExtensionMapper.CreateExtension(dto, dto.ProductId, _productService);
            _context.Extensions.Add(extension);
            await _context.SaveChangesAsync();
            return ExtensionMapper.ToExtensionDTO(extension);
        }

        public async Task<bool> UpdateAsync(UpdateExtensionDTO dto)
        {
            var extension = await _context.Extensions.FindAsync(dto.Id);
            if (extension == null)
            {
                throw new Exception("Extension not found.");
            }

            extension.Title = dto.Title;
            extension.MetaTitle = dto.MetaTitle;
            extension.MetaDescription = dto.MetaDescription;
            extension.Slug = dto.Slug;
            extension.Parentid = dto.Parentid;
            extension.Status = dto.Status;
            extension.UpdatedDate = DateTime.Now;

            _context.Extensions.Update(extension);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ExtensionDTO> GetByIdAsync(int id)
        {
            var extension = await _context.Extensions
                .Include(e => e.ProductExtensions)
                .ThenInclude(pe => pe.PidNavigation) // Ensure Product is loaded
                .FirstOrDefaultAsync(e => e.Id == id);

            if (extension == null)
            {
                throw new Exception("Extension not found.");
            }

            return ExtensionMapper.ToExtensionDTO(extension);
        }

        public async Task<List<ExtensionDTO>> GetAllAsync()
        {
            var extensions = await _context.Extensions
                .Include(e => e.ProductExtensions)
                .ThenInclude(pe => pe.PidNavigation)
                .ToListAsync();

            return extensions.Select(ExtensionMapper.ToExtensionDTO).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var extension = await _context.Extensions.FindAsync(id);
            if (extension == null)
            {
                throw new Exception("Extension not found.");
            }

            // Nếu bạn chỉ muốn đánh dấu là đã xóa, thay vì xóa thật sự
            extension.Isdelete = true;
            _context.Extensions.Update(extension);

            // Nếu bạn muốn xóa thật sự khỏi DB
            // _context.Extensions.Remove(extension);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
