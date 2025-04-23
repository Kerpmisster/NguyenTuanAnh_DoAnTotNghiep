using QLNT_API.DTO.Extension;

namespace QLNT_API.Services.ExtensionServices
{
    public interface IExtensionService
    {
        Task<ExtensionDTO> CreateAsync(CreateExtensionDTO dto);
        Task<bool> UpdateAsync(UpdateExtensionDTO dto);
        Task<ExtensionDTO> GetByIdAsync(int id);
        Task<List<ExtensionDTO>> GetAllAsync();
        Task<bool> DeleteAsync(int id);  // Thêm phương thức Delete
    }
}
