using AutoMapper;
using NZWalksRegions.API.Models.Domain;
using NZWalksRegions.API.Models.DTO;


namespace NZWalksRegions.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
        }
    }

}
