using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LondonAPI.Models;

namespace LondonAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(d => d.Rate,
                    opt => opt.MapFrom(src => src.Rate / 100.0m));
            //TODO Url.Link
        }
    }
}
