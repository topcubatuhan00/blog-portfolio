namespace Blog.Domain.Models.Category;

public class UpdateCategoryModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}
