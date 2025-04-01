using AutoMapper;
using Curotec.Application.DTOs;
using Curotec.Domain;
using Curotec.Domain.Enums;

namespace Curotec.Application.Mappers
{
    public class DTOsToDomainMappingProfile : Profile
    {
        public DTOsToDomainMappingProfile()
        {
            CreateMap<TodoRequest, Todo>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TaskStatusEnum.Pending)) 
                 .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now)); 
        }
    }
}