namespace Blog.Domain.Models.Skill;

public class CreateSkillModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string CreatedBy { get; set; }
}
