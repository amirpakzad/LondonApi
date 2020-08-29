using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Models;
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
                    href=Link.To(nameof(RoomsController.GetRooms))
                },
                info =new
                {
                    href = Link.To(nameof(InfoController.GetInfo))
                }
              
            };
            return (Ok(response));
        }
    }
}
