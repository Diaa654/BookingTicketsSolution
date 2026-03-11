using AutoMapper;
using Domain.Modules;
using Domain.Modules.TripModule;
using Shared.Dtos.TripDto;
using Shared.DTOs.TripDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.TripProfiles
{
    public class TripMapping:Profile
    {
        public TripMapping()
        {
            CreateMap<CreateTripDto, Trip>()
            
            .ForMember(dest => dest.CityTrips, opt => opt.Ignore())
            .ForMember(dest => dest.busId, opt => opt.Ignore())
            .ForMember(dest => dest.DriverId, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.BookedSeats, opt => opt.MapFrom(src => 0))
            .ForMember(dest=>dest.AvailableSeats,opt=>opt.Ignore())
            .ForMember(dest=>dest.tickets,opt=>opt.Ignore());

           

            CreateMap<Trip, TripDto>()
          .ForMember(dest => dest.BusNumber,
              opt => opt.MapFrom(src => src.bus.PlateNumber))
          .ForMember(dest => dest.TotalSeats,
              opt => opt.MapFrom(src => src.bus.Capacity))

          // From City
          .ForMember(dest => dest.FromCityId,
              opt => opt.MapFrom(src => src.CityTrips.FirstOrDefault().FromCityId))
          .ForMember(dest => dest.FromCityName,
              opt => opt.MapFrom(src => src.CityTrips.FirstOrDefault().FromCity.Name))
          // To City
          .ForMember(dest => dest.ToCityId,
              opt => opt.MapFrom(src => src.CityTrips.FirstOrDefault().ToCityId))
          .ForMember(dest => dest.ToCityName,
              opt => opt.MapFrom(src => src.CityTrips.FirstOrDefault().ToCity.Name))
          .ForMember(dest => dest.DriverName,
              opt => opt.MapFrom(src => src.Driver.User.FirstName + " " + src.Driver.User.LastName));
            CreateMap<UpdateTripDto, Trip>()
            .ForMember(dest => dest.CityTrips, opt => opt.Ignore())
            .ForMember(dest => dest.bus, opt => opt.Ignore())
            .ForMember(dest => dest.BookedSeats, opt => opt.Ignore())
            .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore())
            .ForMember(dest => dest.tickets, opt => opt.Ignore());

        }
    }
}
