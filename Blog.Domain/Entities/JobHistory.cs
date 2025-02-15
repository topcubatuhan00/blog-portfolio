namespace Blog.Domain.Entities;

public class JobHistory : BaseEntity
{
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Position { get; set; }
    public bool? IsContinue { get; set; }
    public string? Location { get; set; }
    public string? Skills { get; set; }
}
