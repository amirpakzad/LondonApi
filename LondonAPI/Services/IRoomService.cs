using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Models;

namespace LondonAPI.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomsAsync(Guid id);
    }
}
