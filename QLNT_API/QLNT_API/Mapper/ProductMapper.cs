using QLNT_API.DTO.Product;
using QLNT_API.DTO.ProductExtension;
using QLNT_API.DTO.ProductImage;
using QLNT_API.DTO.ProductMaterial;
using QLNT_API.Models;

namespace QLNT_API.Mapper
{
    public class ProductMapper
    {
        public static async Task<Product> ToEntityAsync(CreateProductDTO dto, string wwwRootPath, int categoryId)
        {
            var product = new Product
            {
                Title = dto.Title,
                Code = dto.Code,
                Description = dto.Description,
                MetaTitle = dto.MetaTitle,
                MetaDescription = dto.MetaDescription,
                Slug = dto.Slug,
                PriceOld = dto.PriceOld,
                PriceNew = dto.PriceNew,
                Home = dto.Home,
                Hot = dto.Hot,
                Status = dto.Status,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Cid = categoryId,
            };

            if (dto.Image != null)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(dto.Image.FileName)}_{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
                var filePath = Path.Combine(wwwRootPath, "Images", "Product", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                product.Image = "/Images/Product/" + fileName;
            }

            return product;
        }

        public static async Task UpdateEntityAsync(Product entity, UpdateProductDTO dto, string wwwRootPath)
        {
            if (!string.IsNullOrWhiteSpace(dto.Title))
                entity.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Code))
                entity.Code = dto.Code;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                entity.Description = dto.Description;

            if (!string.IsNullOrWhiteSpace(dto.MetaTitle))
                entity.MetaTitle = dto.MetaTitle;

            if (!string.IsNullOrWhiteSpace(dto.MetaDescription))
                entity.MetaDescription = dto.MetaDescription;

            if (!string.IsNullOrWhiteSpace(dto.Slug))
                entity.Slug = dto.Slug;

            if (dto.PriceOld.HasValue)
                entity.PriceOld = dto.PriceOld.Value;

            if (dto.PriceNew.HasValue)
                entity.PriceNew = dto.PriceNew.Value;

            if (dto.Home.HasValue)
                entity.Home = dto.Home.Value;

            if (dto.Hot.HasValue)
                entity.Hot = dto.Hot.Value;

            if (dto.Status.HasValue)
                entity.Status = dto.Status.Value;

            if (dto.Cid.HasValue)
                entity.Cid = dto.Cid.Value;

            entity.UpdatedDate = DateTime.Now;


            if (dto.Image != null)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(dto.Image.FileName)}_{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
                var filePath = Path.Combine(wwwRootPath, "Images", "Product", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                entity.Image = "/Images/Product/" + fileName;
            }
        }

        public static ProductDTO ToProductDTO(Product entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Cid = entity.Cid,
                Code = entity.Code,
                Title = entity.Title,
                Description = entity.Description,
                Image = entity.Image,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                PriceOld = entity.PriceOld,
                PriceNew = entity.PriceNew,
                Views = entity.Views,
                Likes = entity.Likes,
                Home = entity.Home,
                Hot = entity.Hot,
                Status = entity.Status,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //ProductImages = entity.ProductImages.Select(img => ProductImageDto.ToProductImageDto(img)).ToList(),
                ProductExtensions = entity.ProductExtensions?.Select(pe => new ProductExtensionDto
                {
                    Id = pe.Id,
                    Pid = pe.Pid,
                    Eid = pe.Eid,
                    // map other properties here
                }).ToList(),

                ProductMaterials = entity.ProductMaterials?.Select(pm => new ProductMaterialDto
                {
                    Id = pm.Id,
                    Pid = pm.Pid,
                    Mid = pm.Mid,
                    // map other properties here
                }).ToList()

            };
        }
    }
}
