using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using LondonAPI.Context;
using LondonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LondonAPI.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiDbContext _context;
        private readonly IMapper _mapper;
        public DefaultRoomService(HotelApiDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Room> GetRoomsAsync(Guid id)
        {
            RoomEntity entity =
                await _context
                    .Rooms
                    .SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null;

            //return new Room()
            //{
            //    Href = null,//Url.Link(nameof(GetRoomById), new { roomId = entity.Id }),
            //    Name = entity.Name,
            //    Rate = entity.Rate / 100.0m
            //};

            return _mapper.Map<Room>(entity);
        }
    }
}
