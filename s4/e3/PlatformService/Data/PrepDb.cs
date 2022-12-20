using PlatformService.Models;

namespace PlatformService.Data
{
 
 public  static class PrepDb
 {
    public static void PrepPopulation(IApplicationBuilder app)
    {
         using( var serviceScope = app.ApplicationServices.CreateScope()){

          SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
         
        }


 
    }

    private static void SeedData(AppDbContext context)
    {
        if(!context.Platforms.Any())
        {

              Console.WriteLine(" data seeding .....");

              context.Platforms.AddRange(
                 new Platform(){ Name ="Dot net", Publisher="Microsfot", Cost="Free"},
                  new Platform(){ Name ="Dot Core", Publisher="Microsfot", Cost="Free"},
                   new Platform(){ Name ="Dot wen api", Publisher="Microsfot", Cost="Free"},
                    new Platform(){ Name ="Dot net core microservice", Publisher="Microsfot", Cost="Free"}
                 
                 );


                 context.SaveChanges();
        }
        else 
        {
             Console.WriteLine("We already have data.");
        }


    }

 }

}