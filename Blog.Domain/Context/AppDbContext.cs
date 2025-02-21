using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<About> Abouts { get; set; }
    public DbSet<Blogs> Blogs { get; set; }
    public DbSet<JobHistory> JobHistorys { get; set; }
    public DbSet<Skill> Skills { get; set; }
}
