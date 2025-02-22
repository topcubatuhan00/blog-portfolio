namespace Blog.Domain.Models.Blog;

public class UpdateBlogModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string UpdatedBy { get; set; }
}
