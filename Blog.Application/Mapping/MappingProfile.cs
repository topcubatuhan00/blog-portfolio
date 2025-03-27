using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Models.About;
using Blog.Domain.Models.Blog;
using Blog.Domain.Models.Category;
using Blog.Domain.Models.JobHistory;
using Blog.Domain.Models.Skill;

namespace Blog.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAboutModel, About>().ReverseMap();
        CreateMap<UpdateAboutModel, About>().ReverseMap();

        CreateMap<CreateBlogModel, Blogs>().ReverseMap();
        CreateMap<UpdateBlogModel, Blogs>().ReverseMap();

        CreateMap<CreateCategoryModel, Category>().ReverseMap();
        CreateMap<UpdateCategoryModel, Category>().ReverseMap();

        CreateMap<CreateJobHistoryModel, JobHistory>().ReverseMap();
        CreateMap<UpdateJobHistoryModel, JobHistory>().ReverseMap();

        CreateMap<CreateSkillModel, Skill>().ReverseMap();
        CreateMap<UpdateSkillModel, Skill>().ReverseMap();
    }
}
