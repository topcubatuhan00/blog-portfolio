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

    public BlogService(AppDbContext appDbContext, IMapper mapper)
    {
        _mapper  = mapper;
        _appDbContext = appDbContext;
    }
    public async Task<bool> AddBlog(CreateBlogModel blog)
    {
        var ent = _mapper.Map<Blogs>(blog);
        ent.CreatedDate = DateTime.Now;
        ent.CreatedBy = "Batuhan Topcu";
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

    public async Task<List<BlogListModel>> GetLastBlogs(int count)
    {
        count = count > 0 ? count : 3;
        var res = _appDbContext.Blogs.Where(p => p.IsActive.Value).OrderByDescending(b => b.Id).Take(count);
        return await res.Select(p => new BlogListModel
        {
            Id = p.Id,
            Title = p.Title,
            CategoryName = _appDbContext.Categories.Where(p => p.Id == p.Id).FirstOrDefault().Name,
            Created = p.CreatedDate.Value,
            ImageUrl = p.ImageUrl,
            Author = p.CreatedBy,
        }).ToListAsync();
    }

    public async Task<HomeDataModel> GetAdminHomeData()
    {
        var today = DateTime.UtcNow.Date;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var now = DateTime.UtcNow;

        var todayCount = await _appDbContext.Blogs
            .CountAsync(b => b.CreatedDate.HasValue && b.CreatedDate.Value.Date == today);

        var thisMonthCount = await _appDbContext.Blogs
            .CountAsync(b => b.CreatedDate.HasValue && b.CreatedDate.Value >= firstDayOfMonth);

        var totalCount = await _appDbContext.Blogs.CountAsync();

        var lastBlogsRaw = await _appDbContext.Blogs
            .OrderByDescending(p => p.CreatedDate)
            .Take(6)
            .Select(p => new
            {
                p.Id,
                p.CreatedDate,
                p.Title
            })
            .ToListAsync();

        var lastBlogs = lastBlogsRaw.Select(p => new HomeBlog
        {
            Id = p.Id,
            Title = p.Title,
            Day = !p.CreatedDate.HasValue ? "" :
                (now.Date - p.CreatedDate.Value.Date).Days == 0 ? "Bugün" :
                $"{(now.Date - p.CreatedDate.Value.Date).Days} gün önce"
        }).ToList();

        return new HomeDataModel
        {
            BlogToday = todayCount,
            BlogMonth = thisMonthCount,
            BlogAll = totalCount,
            LastBlogs = lastBlogs
        };
    }


    public async Task<BlogListModel> GetBlog(int id)
    {
        var res = await (from p in _appDbContext.Blogs
            join c in _appDbContext.Categories on p.CatId equals c.Id
            where p.Id == id
            select new BlogListModel
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.CreatedBy,
                Created = p.CreatedDate ?? DateTime.MinValue,
                Content = p.Content,
                CatId = p.CatId,
                ImageUrl = p.ImageUrl,
                CategoryName = c.Name,
                IsActive = p.IsActive.Value
            }).FirstOrDefaultAsync();
        
        return res;
    }

    public async Task<List<Blogs>> GetBlogs(bool isAdmin = false)
    {
        if (!isAdmin)
        {
            return _appDbContext.Blogs.Where(p => p.IsActive.Value).ToList();
        }
        
        return _appDbContext.Blogs.ToList();
    }

    public async Task<bool> UpdateBlog(UpdateBlogModel blog)
    {
        var ent = _mapper.Map<Blogs>(blog);
        var res = _appDbContext.Blogs.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
