using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistance.Concrete;

public class BlogService : IBlogService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public BlogService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<bool> AddBlog(CreateBlogModel blog)
    {
        var ent = _mapper.Map<Blogs>(blog);
        var res = await _appDbContext.Blogs.AddAsync(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBlog(int id)
    {
        var res = await _appDbContext.Blogs.FindAsync(id);
        if (res != null)
        {
            _appDbContext.Blogs.Remove(res);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Blogs> GetBlog(int id)
    {
        var res = await _appDbContext.Blogs.FindAsync(id);
        return res;
    }

    public async Task<List<Blogs>> GetBlogs()
    {
        var res = await _appDbContext.Blogs.ToListAsync();
        return res;
    }

    public async Task<bool> UpdateBlog(UpdateBlogModel blog)
    {
        var ent = _mapper.Map<Blogs>(blog);
        var res = _appDbContext.Blogs.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
