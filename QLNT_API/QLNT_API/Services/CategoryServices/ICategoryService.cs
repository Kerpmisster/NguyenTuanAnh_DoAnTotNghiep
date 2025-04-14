using QLNT_API.DTO.Category;

namespace QLNT_API.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto, string wwwRootPath);
        Task UpdateAsync(int id, UpdateCategoryDTO dto, string wwwRootPath);
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> AddChildCategoryAsync(int parentId, CreateCategoryDTO dto, string wwwRootPath);
        Task<bool> DeleteAsync(int id);
        Task<bool> ForceDeleteAsync(int id); // nếu bạn thêm xóa thật luôn
    }
}
