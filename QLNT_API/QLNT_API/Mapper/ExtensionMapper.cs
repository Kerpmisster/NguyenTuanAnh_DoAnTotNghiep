using QLNT_API.DTO.Extension;
using QLNT_API.DTO.Product;
using QLNT_API.DTO.ProductExtension;
using QLNT_API.Models;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Mapper
{
    public static class ExtensionMapper
    {
        public static async Task<Extension> CreateExtension(CreateExtensionDTO dto, int productId, IProductService productService)
        {
            var exists = await productService.ProductExists(productId);
            if (!exists)
            {
                throw new Exception("Product does not exist.");
            }

            return new Extension
            {
                Title = dto.Title,
                MetaTitle = dto.MetaTitle,
                MetaDescription = dto.MetaDescription,
                Slug = dto.Slug,
                Parentid = dto.Parentid,
                Status = dto.Status,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Isdelete = false,
                ProductExtensions = new List<ProductExtension>
                {
                    new ProductExtension { Pid = productId }
                }
            };
        }

        public static void UpdateExtension(Extension entity, UpdateExtensionDTO dto)
        {
            entity.Title = dto.Title;
            entity.MetaTitle = dto.MetaTitle;
            entity.MetaDescription = dto.MetaDescription;
            entity.Slug = dto.Slug;
            entity.Parentid = dto.Parentid;
            entity.Status = dto.Status;
            entity.UpdatedDate = DateTime.Now;
        }

        public static ExtensionDTO ToExtensionDTO(Extension entity)
        {
            return new ExtensionDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                Parentid = entity.Parentid,
                Status = entity.Status,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                Children = entity.InverseParent?.Select(ToExtensionDTO).ToList() ?? new List<ExtensionDTO>(),
                ProductExtensions = entity.ProductExtensions?
                    .Where(pe => pe.PidNavigation != null)
                    .Select(pe => new ProductExtensionDto
                    {
                        Pid = pe.Pid,
                        Product = ProductMapper.ToProductDTO(pe.PidNavigation)
                    })
                    .ToList() ?? new List<ProductExtensionDto>()
                //Products = entity.Products?.Select(p => ProductMapper.ToProductDTO(p)).ToList() ?? new List<ProductDTO>()
            };
        }
    }
}
