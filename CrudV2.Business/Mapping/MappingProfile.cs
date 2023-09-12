using AutoMapper;
using CrudV2.Business.DTOs;
using CrudV2.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}