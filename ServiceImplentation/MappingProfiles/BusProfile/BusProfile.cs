using AutoMapper;
using Domain.Modules.BusModule;
using Shared.DTOs.BusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.BusProfile
{
    internal class BusProfile:Profile
    {
        public BusProfile()
        {
            CreateMap<CreateBusDto,Bus>().
                ForMember(dest => dest.Trips, opt => opt.Ignore());
            CreateMap<Bus, BusDto>()
               .ForMember(dest => dest.TripCount,
                   opt => opt.MapFrom(src => src.Trips.Count));
            CreateMap<UpdateBusDto, Bus>()
                .ForMember(dest => dest.Trips, opt => opt.Ignore());

        }
    }
}
