using Microsoft.EntityFrameworkCore;
using QLNT_API.DTO.Material;
using QLNT_API.Mapper;
using QLNT_API.Models;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Services.MaterialServices
{
    public class MaterialService : IMaterialService
    {
        private readonly QuanlynoithatContext _context;
        private readonly IProductService _productService;

        public MaterialService(QuanlynoithatContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<MaterialDTO> CreateAsync(CreateMaterialDTO dto)
        {
            var material = await MaterialMapper.CreateMaterial(dto, dto.ProductId, _productService);
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            return MaterialMapper.ToMaterialDTO(material);
        }

        public async Task<List<MaterialDTO>> GetAllAsync()
        {
            var material = await _context.Materials
               .Include(e => e.ProductMaterials)
               .ThenInclude(pe => pe.PidNavigation)
               .ToListAsync();

            return material.Select(MaterialMapper.ToMaterialDTO).ToList();
        }

        public async Task<MaterialDTO> GetByIdAsync(int id)
        {
            var material = await _context.Materials
                .Include(e => e.ProductMaterials)
                .ThenInclude(pe => pe.PidNavigation) // Ensure Product is loaded
                .FirstOrDefaultAsync(e => e.Id == id);

            if (material == null)
            {
                throw new Exception("material not found.");
            }

            return MaterialMapper.ToMaterialDTO(material);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                throw new Exception("material not found.");
            }

            // Nếu bạn chỉ muốn đánh dấu là đã xóa, thay vì xóa thật sự
            material.Isdelete = true;
            _context.Materials.Update(material);

            // Nếu bạn muốn xóa thật sự khỏi DB
            // _context.materials.Remove(material);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateMaterialDTO dto)
        {
            var material = await _context.Materials.FindAsync(dto.Id);
            if (material == null)
            {
                throw new Exception("material not found.");
            }

            material.Title = dto.Title;
            material.MetaTitle = dto.MetaTitle;
            material.MetaDescription = dto.MetaDescription;
            material.Slug = dto.Slug;
            material.Parentid = dto.Parentid;
            material.Status = dto.Status;
            material.UpdatedDate = DateTime.Now;

            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
