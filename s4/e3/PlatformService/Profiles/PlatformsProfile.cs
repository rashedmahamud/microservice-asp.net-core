using AutoMapper;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Platforms
{

     public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // source==> target 
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformReadDto, Platform>();


            CreateMap<Platform, PlatformCreateDto>();
            CreateMap<PlatformCreateDto, Platform>();
            
        }
    }

}