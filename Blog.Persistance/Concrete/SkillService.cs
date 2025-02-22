using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.Skill;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistance.Concrete;

public class SkillService : ISkillService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public SkillService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<bool> AddSkill(CreateSkillModel skill)
    {
        var ent = _mapper.Map<Skill>(skill);
        var res = await _appDbContext.Skills.AddAsync(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteSkill(int id)
    {
        var res = await _appDbContext.Skills.FindAsync(id);
        if (res != null)
        {
            _appDbContext.Skills.Remove(res);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Skill> GetSkill(int id)
    {
        var res = await _appDbContext.Skills.FindAsync(id);
        return res;
    }

    public async Task<List<Skill>> GetSkills()
    {
        var res = await _appDbContext.Skills.ToListAsync();
        return res;
    }

    public async Task<bool> UpdateSkill(UpdateSkillModel skill)
    {
        var ent = _mapper.Map<Skill>(skill);
        var res = _appDbContext.Skills.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
