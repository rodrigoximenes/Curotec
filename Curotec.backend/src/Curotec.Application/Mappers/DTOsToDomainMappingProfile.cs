using AutoMapper;
using Curotec.Application.DTOs;
using Curotec.Domain;

namespace Curotec.Application.Mappers
{
    public class DTOsToDomainMappingProfile : Profile
    {
        public DTOsToDomainMappingProfile()
        {
            CreateMap<TodoRequest, Todo>()
                 .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now)); 
        }
    }
}