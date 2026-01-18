namespace Blog.Domain.Models.Category;

public class CreateCategoryModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public string? CreatedBy { get; set; }
}
