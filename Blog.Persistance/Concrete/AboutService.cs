using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.About;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistance.Concrete;

public class AboutService : IAboutService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public AboutService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> AddAbout(CreateAboutModel about)
    {
        var ent = _mapper.Map<About>(about);
        var res = await _appDbContext.Abouts.AddAsync(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAbout(int id)
    {
        var res = await _appDbContext.Abouts.FindAsync(id);
        if (res != null)
        {
            _appDbContext.Abouts.Remove(res);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<About> GetAbout(int id)
    {
        var res = await _appDbContext.Abouts.FindAsync(id);
        return res;
    }

    public async Task<List<About>> GetAbouts()
    {
        var res = await _appDbContext.Abouts.ToListAsync();
        return res;
    }

    public async Task<bool> UpdateAbout(UpdateAboutModel about)
    {
        var ent = _mapper.Map<About>(about);
        var res = _appDbContext.Abouts.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
