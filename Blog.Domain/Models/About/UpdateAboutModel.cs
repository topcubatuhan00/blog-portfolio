namespace Blog.Domain.Models.About;

public class UpdateAboutModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UpdatedBy { get; set; }
}
