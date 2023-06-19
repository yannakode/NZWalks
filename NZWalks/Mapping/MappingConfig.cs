using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using NZWalks.Models.DTO;

namespace NZWalks.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        }
    }
}
