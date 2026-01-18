namespace Blog.Domain.Models.JobHistory;

public class UpdateJobHistoryModel
{
    public int Id { get; set; }
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Position { get; set; }
    public bool? IsContinue { get; set; }
    public string? Location { get; set; }
    public string? Skills { get; set; }
    public string? UpdatedBy { get; set; }
}
