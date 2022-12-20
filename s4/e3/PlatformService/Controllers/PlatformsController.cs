using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{

    [Route("api/Platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {

        private readonly IPlatformRepo _repository; 
        private readonly IMapper _mapper; 

        private readonly ICommandDataClient _commandDataClient;  
        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository; 
            _mapper =  mapper; 
            _commandDataClient = commandDataClient;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>>  GetPlatforms()
        {
              Console.WriteLine("--> Getting Platforms.....");
             var platforms=   _repository.GetAllPlatforms();        
            
             return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        { 
              
               var platform = _repository.GetPlatformById(id); 
               

               if (platform !=null)
               {
                   return Ok(_mapper.Map<PlatformReadDto>(platform));
               }
               return NotFound();

        }

        [HttpPost]
        public  async  Task<ActionResult<PlatformCreateDto>> CreatePlatform (PlatformCreateDto  platformCreateDto )
        {
                 var platform = _mapper.Map<Platform>(platformCreateDto);
                 
                 _repository.CreatePlatform(platform);
                 _repository.SaveChanges();

                 var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
                  try{
                     await _commandDataClient.SendPlatformToCommand(platformReadDto);
                  }
                  catch(Exception ex)
                  {
                     Console.WriteLine($"-->Could not send sysncronusly {ex.Message}");
                  }

                 return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformReadDto.Id}, platformReadDto);

        }
    }

}