using QLNT_API.DTO.Menu;
using QLNT_API.Models;

namespace QLNT_API.Mapper
{
    public static class MenuMapper
    {
        public static async Task<Menu> ToEntityAsync(CreateMenuDTO dto)
        {
            var menu = new Menu
            {
                Title = dto.Title,
                Slug = !string.IsNullOrWhiteSpace(dto.Slug) ? dto.Slug : GenerateSlug(dto.Title),
                Parentid = dto.ParentId,
                Position = dto.Position,
                Isactive = (byte)(dto.IsActive ? 1 : 0)
            };

            // Vì Menu không có ảnh hoặc thời gian tạo nên ta không cần xử lý thêm
            return await Task.FromResult(menu);
        }

        public static async Task UpdateEntityAsync(Menu entity, UpdateMenuDTO dto)
        {
            if (dto.Title != null)
            {
                entity.Title = dto.Title;
                if (string.IsNullOrWhiteSpace(dto.Slug))
                    entity.Slug = GenerateSlug(dto.Title);
            }

            if (dto.Slug != null)
                entity.Slug = dto.Slug;

            if (dto.ParentId.HasValue)
                entity.Parentid = dto.ParentId;

            if (dto.Position.HasValue)
                entity.Position = dto.Position;

            if (dto.IsActive.HasValue)
                entity.Isactive = (byte)(dto.IsActive.Value ? 1 : 0);

            await Task.CompletedTask;
        }

        public static MenuDTO ToDTO(Menu entity)
        {
            return new MenuDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                ParentId = entity.Parentid,
                Position = entity.Position,
                IsActive = entity.Isactive == 1,
                Children = new List<MenuDTO>() // Có thể gán sau nếu xử lý đệ quy
            };
        }

        private static string GenerateSlug(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;

            var normalized = title.ToLowerInvariant()
                                  .Replace("đ", "d")
                                  .Replace(" ", "-");

            normalized = System.Text.RegularExpressions.Regex.Replace(normalized, @"[^a-z0-9\-]", "");
            normalized = System.Text.RegularExpressions.Regex.Replace(normalized, @"-+", "-");

            return normalized.Trim('-');
        }
    }
}
