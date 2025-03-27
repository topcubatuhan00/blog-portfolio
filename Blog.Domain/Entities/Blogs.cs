namespace Blog.Domain.Entities;

public class Blogs : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int CatId { get; set; }
}
