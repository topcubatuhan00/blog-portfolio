using Blog.Domain.Entities;
using Blog.Domain.Models.Skill;

namespace Blog.Application.Abstract;

public interface ISkillService
{
    Task<bool> AddSkill(CreateSkillModel skill);
    Task<bool> UpdateSkill(UpdateSkillModel skill);
    Task<bool> DeleteSkill(int id);
    Task<Skill> GetSkill(int id);
    Task<List<Skill>> GetSkills();
}
