using AutoMapper;
using Hotels.Application.DTOs;
using Hotels.Domain.Entities;

namespace Hotels.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.Image != null ? src.Image.File : null))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner));

            CreateMap<Owner, OwnerDto>();
        }
    }
}
