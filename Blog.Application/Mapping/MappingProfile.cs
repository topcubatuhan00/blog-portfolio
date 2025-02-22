using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Models.About;

namespace Blog.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAboutModel, About>().ReverseMap();
    }
}
