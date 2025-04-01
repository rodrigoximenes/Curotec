using AutoMapper;
using Curotec.Application.DTOs;
using Curotec.Domain;

namespace Curotec.Application.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Todo, TodoResponse>();
        }
    }
}
