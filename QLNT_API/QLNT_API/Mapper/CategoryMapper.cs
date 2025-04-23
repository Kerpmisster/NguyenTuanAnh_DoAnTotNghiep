using QLNT_API.DTO.Category;
using QLNT_API.DTO.Product;
using QLNT_API.Models;

namespace QLNT_API.Mapper
{
    public class CategoryMapper
    {
        public static async Task<Category> ToEntityAsync(CreateCategoryDTO dto, string wwwRootPath)
        {
            var category = new Category
            {
                Title = dto.Title,
                MetaTitle = dto.MetaTitle,
                MetaKeyword = dto.MetaKeyword,
                MetaDescription = dto.MetaDescription,
                Slug = dto.Slug,
                Orders = dto.Orders,
                Parentid = dto.ParentId,
                Status = dto.Status,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Isdelete = false
            };

            if (dto.Icon != null)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(dto.Icon.FileName)}_{Guid.NewGuid()}{Path.GetExtension(dto.Icon.FileName)}";
                var filePath = Path.Combine(wwwRootPath, "Images", "Category", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Icon.CopyToAsync(stream);
                }

                category.Icon = "/Images/Category/" + fileName;
            }

            return category;
        }

        public static async Task UpdateEntityAsync(Category entity, UpdateCategoryDTO dto, string wwwRootPath)
        {
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.MetaTitle != null) entity.MetaTitle = dto.MetaTitle;
            if (dto.MetaKeyword != null) entity.MetaKeyword = dto.MetaKeyword;
            if (dto.MetaDescription != null) entity.MetaDescription = dto.MetaDescription;
            if (dto.Slug != null) entity.Slug = dto.Slug;
            if (dto.Orders.HasValue) entity.Orders = dto.Orders.Value;
            if (dto.ParentId.HasValue) entity.Parentid = dto.ParentId.Value;
            if (dto.Status.HasValue) entity.Status = dto.Status.Value;

            entity.UpdatedDate = DateTime.Now;

            if (dto.Icon != null)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(dto.Icon.FileName)}_{Guid.NewGuid()}{Path.GetExtension(dto.Icon.FileName)}";
                var filePath = Path.Combine(wwwRootPath, "Images", "Category", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Icon.CopyToAsync(stream);
                }

                entity.Icon = "/Images/Category/" + fileName;
            }
        }

        public static CategoryDTO ToDTO(Category entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Icon = entity.Icon,
                MetaTitle = entity.MetaTitle,
                MetaKeyword = entity.MetaKeyword,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                Orders = entity.Orders,
                ParentId = entity.Parentid,
                Status = entity.Status,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                Children = entity.InverseParent?.Select(ToDTO).ToList() ?? new List<CategoryDTO>(),
                Products = entity.Products?.Select(p => ProductMapper.ToProductDTO(p)).ToList() ?? new List<ProductDTO>()
            };
        }
    }
}
