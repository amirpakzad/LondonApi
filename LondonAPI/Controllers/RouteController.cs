﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LondonAPI.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RouteController :ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href=Url.Link(nameof(GetRoot),null),
                rooms= new
                {
                    href=Url.Link(nameof(RoomsController.GetRooms),null)
                }
            };
            return (Ok(response));
        }
    }
}