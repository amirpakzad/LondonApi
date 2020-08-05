using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LondonAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController :ControllerBase
    {
        private readonly HotelInfo _hotelInfo;

        public InfoController(IOptions<HotelInfo> hotelInfoWrapper)
        {
            _hotelInfo = hotelInfoWrapper.Value;
        }
        [HttpGet(Name=nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<HotelInfo> GetInfo()
        {
            _hotelInfo.Href = Url.Link(nameof(GetInfo), null);
            return _hotelInfo;
        }
    }
}
