using Blog.Domain.Entities;

namespace Blog.Domain.Models.Admin;

public class HomeDataModel
{
    public int BlogToday { get; set; }
    public int BlogMonth { get; set; }
    public int BlogAll { get; set; }
    public List<HomeBlog> LastBlogs { get; set; } 
}
