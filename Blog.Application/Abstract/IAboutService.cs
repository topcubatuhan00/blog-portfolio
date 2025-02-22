using Blog.Domain.Entities;
using Blog.Domain.Models.About;

namespace Blog.Application.Abstract;

public interface IAboutService
{
    Task<bool> AddAbout(CreateAboutModel about);
    Task<bool> UpdateAbout(UpdateAboutModel about);
    Task<bool> DeleteAbout(int id);
    Task<About> GetAbout(int id);
    Task<List<About>> GetAbouts();
}
