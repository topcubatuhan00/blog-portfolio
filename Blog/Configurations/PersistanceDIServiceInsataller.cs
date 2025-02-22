
using Blog.Application.Abstract;
using Blog.Application.Mapping;
using Blog.Persistance.Concrete;

namespace Blog.Configurations;

public static class PersistanceDIServiceInsataller
{
    public static void AddServiceExtensions(this IServiceCollection services)
    {
        services.AddScoped<IAboutService, AboutService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IJobHistoryService, JobHistoryService>();
        services.AddScoped<ISkillService, SkillService>();

        //services.AddAutoMapper(typeof(Blog.Persistance.AssemblyReference).Assembly);
        services.AddAutoMapper(typeof(MappingProfile));
    }
}
