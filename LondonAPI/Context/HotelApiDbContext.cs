using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Models;

namespace LondonAPI.Context
{
    public class HotelApiDbContext :DbContext
    {
        public HotelApiDbContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
