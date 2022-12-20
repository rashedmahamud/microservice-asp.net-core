using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly  AppDbContext  _appDbContext;

        public PlatformRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CreatePlatform(Platform plat)
        {
           if(plat==null)
           {
                throw new ArgumentException(nameof(plat));
           }
           else
           {
              _appDbContext.Add(plat);
           }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
           return _appDbContext.Platforms.ToList();
        }

        public Platform GetPlatformById(int Id)
        {
           return _appDbContext.Platforms.SingleOrDefault(x=>x.Id ==Id);
        }

        public bool SaveChanges()
        {
           return (_appDbContext.SaveChanges()>=0);
        }
    } 

}