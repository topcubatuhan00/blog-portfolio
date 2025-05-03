using Blog.Domain.Entities;
using Blog.Domain.Models.Admin;
using Blog.Domain.Models.Blog;

namespace Blog.Application.Abstract;

public interface IBlogService
{
    Task<bool> AddBlog(CreateBlogModel blog);
    Task<bool> UpdateBlog(UpdateBlogModel blog);
    Task<bool> DeleteBlog(int id);
    Task<BlogListModel> GetBlog(int id);
    Task<List<Blogs>> GetBlogs(bool isAdmin);
    Task<List<BlogListModel>> GetLastBlogs(int count);
    Task<HomeDataModel> GetAdminHomeData();
}
