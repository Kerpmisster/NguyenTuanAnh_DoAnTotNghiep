using QLNT_API.DTO.Product;

namespace QLNT_API.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductDTO> GetByIdAsync(int id);
        Task<List<ProductDTO>> GetAllAsync();
        Task<List<ProductDTO>> GetProductsAsync(FilterDTO filter);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto, string wwwRootPath);
        Task<bool> UpdateAsync(int id, UpdateProductDTO dto, string wwwRootPath);
        Task<bool> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
    }
}
