using QLNT_API.DTO.Material;

namespace QLNT_API.Services.MaterialServices
{
    public interface IMaterialService
    {
        Task<MaterialDTO> CreateAsync(CreateMaterialDTO dto);
        Task<bool> UpdateAsync(UpdateMaterialDTO dto);
        Task<MaterialDTO> GetByIdAsync(int id);
        Task<List<MaterialDTO>> GetAllAsync();
        Task<bool> DeleteAsync(int id);  // Thêm phương thức Delete
    }
}
