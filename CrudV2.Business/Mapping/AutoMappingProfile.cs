using AutoMapper;
using CrudV2.Business.DTOs;
using CrudV2.Core.Entities;

namespace CrudV2.Business.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<User, UserDto>(); // Exemplo de mapeamento entre User e UserDto
        }
    }
}
