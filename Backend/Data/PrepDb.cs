using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<BackendContext>());
            }
        }

        public static void SeedData(BackendContext context)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();

        }
    }
}