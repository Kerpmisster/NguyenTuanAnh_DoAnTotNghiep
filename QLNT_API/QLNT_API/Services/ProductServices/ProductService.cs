using Microsoft.EntityFrameworkCore;
using QLNT_API.DTO.Product;
using QLNT_API.Mapper;
using QLNT_API.Models;

namespace QLNT_API.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly QuanlynoithatContext _context;
        public ProductService(QuanlynoithatContext context)
        {
            _context = context;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");
            return ProductMapper.ToProductDTO(product);
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(ProductMapper.ToProductDTO).ToList();
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto, string wwwRootPath)
        {
            if (!dto.Cid.HasValue)
                throw new Exception("Category ID (Cid) is required.");

            var product = await ProductMapper.ToEntityAsync(dto, wwwRootPath, dto.Cid.Value);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return ProductMapper.ToProductDTO(product);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDTO dto, string wwwRootPath)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Sản phẩm not không tìm thấy");

            await ProductMapper.UpdateEntityAsync(product, dto, wwwRootPath);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<List<ProductDTO>> GetProductsAsync(FilterDTO filter)
        {
            var query = _context.Products.AsQueryable();
            // Chỉ lấy sản phẩm chưa bị xóa
            query = query.Where(p => p.Isdelete == false || p.Isdelete == null);

            if (filter != null)
            {
                if (filter.CategoryId.HasValue && filter.CategoryId.Value > 0)
                {
                    query = query.Where(p => p.Cid == filter.CategoryId.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.Title) && filter.Title.ToLower() != "string")
                {
                    query = query.Where(p => p.Title != null && p.Title.Contains(filter.Title));
                }

                if (filter.PriceMin.HasValue && filter.PriceMin.Value > 0)
                {
                    query = query.Where(p => p.PriceNew >= filter.PriceMin.Value);
                }

                if (filter.PriceMax.HasValue && filter.PriceMax.Value > 0)
                {
                    query = query.Where(p => p.PriceNew <= filter.PriceMax.Value);
                }
            }

            var products = await query.ToListAsync();
            return products.Select(p => ProductMapper.ToProductDTO(p)).ToList();
        }
    }
}
