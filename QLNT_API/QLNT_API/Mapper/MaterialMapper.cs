using QLNT_API.DTO.Material;
using QLNT_API.DTO.ProductMaterial;
using QLNT_API.Models;
using QLNT_API.Services.ProductServices;

namespace QLNT_API.Mapper
{
    public class MaterialMapper
    {
        public static async Task<Material> CreateMaterial(CreateMaterialDTO dto, int productId, IProductService productService)
        {
            var exists = await productService.ProductExists(productId);
            if (!exists)
            {
                throw new Exception("Product does not exist.");
            }

            var material = new Material
            {
                Title = dto.Title,
                MetaTitle = dto.MetaTitle,
                MetaDescription = dto.MetaDescription,
                Slug = dto.Slug,
                Parentid = dto.Parentid,
                Status = dto.Status,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ProductMaterials = new List<ProductMaterial>
                {
                    new ProductMaterial { Pid = productId }
                }
            };
            return material;
        }

        public static void UpdateMaterial(Material entity, UpdateMaterialDTO dto)
        {
            entity.Title = dto.Title;
            entity.MetaTitle = dto.MetaTitle;
            entity.MetaDescription = dto.MetaDescription;
            entity.Slug = dto.Slug;
            entity.Parentid = dto.Parentid;
            entity.Status = dto.Status;
            entity.UpdatedDate = DateTime.Now;
        }

        public static MaterialDTO ToMaterialDTO(Material entity)
        {
            return new MaterialDTO
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
                Children = entity.InverseParent?.Select(ToMaterialDTO).ToList() ?? new List<MaterialDTO>(),
                ProductMaterials = entity.ProductMaterials?
                    .Select(pe => new ProductMaterialDto
                    {
                        Pid = pe.Pid,
                        Product = ProductMapper.ToProductDTO(pe.PidNavigation)
                    })
                    .ToList() ?? new List<ProductMaterialDto>()
                //Products = entity.Products?.Select(p => ProductMapper.ToProductDTO(p)).ToList() ?? new List<ProductDTO>()
            };
        }
    }
}
