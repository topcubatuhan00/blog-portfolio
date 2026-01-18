using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistance.Concrete;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<bool> AddCategory(CreateCategoryModel category)
    {
        var ent = _mapper.Map<Category>(category);
        var res = await _appDbContext.Categories.AddAsync(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var res = await _appDbContext.Categories.FindAsync(id);
        if (res != null)
        {
            _appDbContext.Categories.Remove(res);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Category>> GetCategories()
    {
        var res = await _appDbContext.Categories.ToListAsync();
        return res;
    }

    public async Task<Category> GetCategory(int id)
    {
        var res = await _appDbContext.Categories.FindAsync(id);
        if (res == null) throw new KeyNotFoundException($"Category not found. Id: {id}");
        return res;
    }

    public async Task<bool> UpdateCategory(UpdateCategoryModel category)
    {
        var ent = _mapper.Map<Category>(category);
        var res = _appDbContext.Categories.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
