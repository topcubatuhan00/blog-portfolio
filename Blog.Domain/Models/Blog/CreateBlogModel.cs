namespace Blog.Domain.Models.Blog;

public class CreateBlogModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public string CreatedBy { get; set; }
    public int CatId { get; set; }
    public bool IsActive { get; set; }
}
