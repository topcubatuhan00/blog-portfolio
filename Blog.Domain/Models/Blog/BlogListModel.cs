namespace Blog.Domain.Models.Blog;

public class BlogListModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public DateTime Created { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public int CatId{ get; set; }
    public bool IsActive { get; set; }
}