using Blog.Domain.Entities;
using Blog.Domain.Models.Admin;
using Blog.Domain.Models.Blog;

namespace Blog.Application.Abstract;

public interface IBlogService
{
    Task<bool> AddBlog(CreateBlogModel blog);
    Task<bool> UpdateBlog(UpdateBlogModel blog);
    Task<bool> DeleteBlog(int id);
    Task<Blogs> GetBlog(int id);
    Task<List<Blogs>> GetBlogs();
    Task<HomeDataModel> GetAdminHomeData();
}
