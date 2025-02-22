using Blog.Domain.Entities;
using Blog.Domain.Models.Category;

namespace Blog.Application.Abstract;

public interface ICategoryService
{
    Task<bool> AddCategory(CreateCategoryModel category);
    Task<bool> UpdateCategory(UpdateCategoryModel category);
    Task<bool> DeleteCategory(int id);
    Task<Category> GetCategory(int id);
    Task<List<Category>> GetCategories();
}
