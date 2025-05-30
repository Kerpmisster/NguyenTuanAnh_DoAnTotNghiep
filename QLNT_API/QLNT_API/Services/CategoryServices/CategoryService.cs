﻿using Microsoft.EntityFrameworkCore;
using QLNT_API.Data;
using QLNT_API.DTO.Category;
using QLNT_API.DTO.Product;
using QLNT_API.Mapper;
using QLNT_API.Models;

namespace QLNT_API.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly QuanlynoithatContext _context;
        public CategoryService(QuanlynoithatContext context)
        {
            _context = context;
        }
        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto, string wwwRootPath)
        {
            var category = await CategoryMapper.ToEntityAsync(dto, wwwRootPath);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CategoryMapper.ToDTO(category) /*await GetByIdAsync(category.Id)*/;
        }

        public async Task UpdateAsync(int id, UpdateCategoryDTO dto, string wwwRootPath)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found");

            await CategoryMapper.UpdateEntityAsync(category, dto, wwwRootPath);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.InverseParent)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category == null ? null : CategoryMapper.ToDTO(category);
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.Isdelete == false && c.Parentid == null)
                .Include(c => c.InverseParent).ThenInclude(c => c.Products)
                .ToListAsync();

            return categories.Select(CategoryMapper.ToDTO).ToList();
        }

        public async Task<CategoryDTO> AddChildCategoryAsync(int parentId, CreateCategoryDTO dto, string wwwRootPath)
        {
            // Tìm category cha
            var parent = await _context.Categories
                .Include(c => c.InverseParent)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == parentId && (c.Isdelete == false || c.Isdelete == null));

            if (parent == null)
                throw new Exception("Parent category not found");

            // Tạo category con
            var child = await CategoryMapper.ToEntityAsync(dto, wwwRootPath);
            child.Parentid = parent.Id;

            // Gán vào danh sách con
            parent.InverseParent ??= new List<Category>();
            parent.InverseParent.Add(child);

            _context.Categories.Add(child);  // Lưu category con
            _context.Categories.Update(parent); // Cập nhật cha

            await _context.SaveChangesAsync();

            // Load lại cha để trả về DTO có children mới nhất
            var updatedParent = await _context.Categories
                .Include(c => c.InverseParent)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == parentId);

            return CategoryMapper.ToDTO(updatedParent!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null || category.Isdelete == true)
                return false;

            category.Isdelete = true;
            category.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ForceDeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<List<CategoryDTO>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<CategoryDTO>();

            keyword = keyword.ToLower();

            var categories = await _context.Categories
                .Where(c =>
                    c.Title.ToLower().Contains(keyword) ||
                    c.Slug.ToLower().Contains(keyword) ||
                    c.MetaTitle.ToLower().Contains(keyword)
                )
                .Where(c => c.Isdelete == false)
                .ToListAsync();

            var result = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Title = c.Title,
                Icon = c.Icon,
                MetaTitle = c.MetaTitle,
                MetaKeyword = c.MetaKeyword,
                MetaDescription = c.MetaDescription,
                Slug = c.Slug,
                Orders = c.Orders,
                ParentId = c.Parentid,
                Status = c.Status,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Children = new List<CategoryDTO>(), // Gán rỗng để tránh lồng vào
                Products = new List<ProductDTO>()    // Gán rỗng nếu chưa cần
            }).ToList();

            return result;
        }
    }
}
