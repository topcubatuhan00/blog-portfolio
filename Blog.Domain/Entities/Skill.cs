namespace Blog.Domain.Entities;

public class Skill : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
}
