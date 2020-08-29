using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Context;
using LondonAPI.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LondonAPI
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<HotelApiDbContext>());
        }
        public static async Task AddTestData(HotelApiDbContext context)
        {
            if (context.Rooms.Any())
            {
                //Already Has Data
                return;
            }
            context.Rooms.Add(new RoomEntity()
            {
                Id =Guid.NewGuid(),
                Name = "Driscoll Suite",
                Rate = 1000
            });
            context.Rooms.Add(new RoomEntity()
            {
                Id =  Guid.Parse("f391e1b4-4e6e-4bd0-aa58-c05f0c580f17"),//Guid(61331889-41f6-4b96-8ff1-6dc090f709f1),
                Name = "ViewBer Mar",
                Rate = 2000
            });
            await context.SaveChangesAsync();
        }
    }
}
