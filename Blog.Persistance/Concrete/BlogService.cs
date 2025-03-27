using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.Admin;
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

    public async Task<HomeDataModel> GetAdminHomeData()
    {
        var today = DateTime.UtcNow.Date; // Bugünün tarihi (sadece tarih kısmı)
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1); // Ayın ilk günü

        var todayCount = await _appDbContext.Blogs
            .CountAsync(b => b.CreatedDate.Value.Date == today); // Bugün eklenenler

        var thisMonthCount = await _appDbContext.Blogs
            .CountAsync(b => b.CreatedDate >= firstDayOfMonth); // Bu ay eklenenler

        var totalCount = await _appDbContext.Blogs.CountAsync(); // Tüm blog sayısı

        var lastBlogs = await _appDbContext.Blogs
            .OrderByDescending(p => p.CreatedDate) // En yeni blogları al
            .Take(6)
            .Select(p => new HomeBlog
            {
                Id = p.Id,
                Day = EF.Functions.DateDiffDay(p.CreatedDate, DateTime.UtcNow) == 0
                    ? "Bugün"
                    : $"{EF.Functions.DateDiffDay(p.CreatedDate, DateTime.UtcNow)} gün önce",
                Title = p.Title
            })
            .ToListAsync();

        var model = new HomeDataModel();

        model.BlogToday = todayCount;
        model.BlogMonth = thisMonthCount;
        model.BlogAll = totalCount;
        model.LastBlogs = lastBlogs;
        return model;
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
