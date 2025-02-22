namespace Blog.Domain.Models.Blog;

public class CreateBlogModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string CreatedBy { get; set; }
}
