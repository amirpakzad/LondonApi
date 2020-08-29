using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Context;
using LondonAPI.Models;
using LondonAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LondonAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RoomsController :ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet(Name=nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }

        //GET   /Room/{roomId}
        [HttpGet("{roomId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        {
            var room = await _roomService.GetRoomsAsync(roomId);
            if (room ==null)
            {
                return NotFound();
            }

            return room;
        }
    }
}
