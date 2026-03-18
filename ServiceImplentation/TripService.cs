using AutoMapper;
using Domain.Contracts;
using Domain.Modules;
using Domain.Modules.BusModule;
using Domain.Modules.CityModule;
using Domain.Modules.TripModule;
using Domain.Modules.UserModule;
using Services.Specifications;
using Services.Specifications.BusSpecifications;
using Services.Specifications.CitySpecifications.cs;
using Services.Specifications.DriverSpecifications;
using Services.Specifications.TripSpecifications;
using ServicesAbstraction;
using Shared;
using Shared.CommonResult;
using Shared.Dtos.TripDto;
using Shared.DTOs.TripDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<string>> CreateTripAsync(CreateTripDto dto)
        {
            var existingTripSpec = new TripWithCItysSpecification(dto.FromCityName, dto.ToCityName, dto.DateOfDeparture);
            var existingTrip = await _unitOfWork.GetRepository<Trip, int>().GetByIdAsync(existingTripSpec);
            if (existingTrip != null) return Error.Conflict("Trip.Conflict", "A trip with the same details already exists.");
            var bus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(new BusSpecifications(dto.BusPlateNumber));
            if (bus == null) return Error.NotFound("Bus.NotFound", "The specified bus was not found.");
            if (bus.Status == 0 )
                return Error.Conflict("Bus.Unavailable", "The specified bus is currently unavailable.");
            if(bus.Trips.Any(t => t.DateOfDeparture == dto.DateOfDeparture))
                return Error.Conflict("Bus.Unavailable", "The specified bus is currently Busy on the selected date.");
            var driver = await _unitOfWork.GetRepository<Driver, int>().GetByIdAsync(new DriverSpecifications(dto.DriverName));
            if (driver == null) return Error.NotFound("Driver.NotFound", "The specified driver was not found.");
            if (driver.IsAvailable == false ) 
                return Error.Conflict("Driver.Unavailable", "The specified driver is currently unavailable.");
            if (driver.DriverTrips.Any(t => t.DateOfDeparture == dto.DateOfDeparture))
                return Error.Conflict("Driver.Unavailable", "The specified Driver is currently Busy on the selected date.");
            var fromCity = await _unitOfWork.GetRepository<City, int>().GetByIdAsync(new CityByNameSpec(dto.FromCityName));
            var toCity = await _unitOfWork.GetRepository<City, int>().GetByIdAsync(new CityByNameSpec(dto.ToCityName));
            if (fromCity == null || toCity == null) return Error.Validation("City.NotFound", "Invalid city names.");
            var trip = _mapper.Map<Trip>(dto);
            trip.bus = bus;
            trip.busId = bus.Id;
            trip.DriverId = driver.UserId;
            trip.Driver = driver;
            trip.CityTrips = new List<CityTrips>
            {
              new CityTrips { FromCityId = fromCity.Id, ToCityId = toCity.Id ,DateOfDeparture=dto.DateOfDeparture}
            };

            await _unitOfWork.GetRepository<Trip, int>().AddAsync(trip);
            await _unitOfWork.SaveChangesAsync();

            return "Trip created successfully.";
        }
        public async Task<Result<TripDto>> GetTripByIdAsync(int id)
        {
            var spec = new TripWithCItysSpecification(id);
            var trip = await _unitOfWork.GetRepository<Trip, int>().GetByIdAsync(spec);
            if (trip is null)
               return Error.NotFound("Trip.NotFound", $"The trip with {id} was not found.");
            var driverspec = new DriverSpecifications(trip.DriverId);
            var driver = await _unitOfWork.GetRepository<Driver, int>().GetByIdAsync(driverspec);
            if (driver is null)
                return Error.NotFound("Driver.NotFound", $"The driver for the trip with {id} was not found.");
            var TripToReturn = _mapper.Map<TripDto>(trip);
            TripToReturn.DriverName = driver.User.FirstName+" " + driver.User.LastName;
            return TripToReturn;
        }

        public async Task<Result<string>> DeleteTripAsync(int id)
        {
            var trip = await _unitOfWork.GetRepository<Trip, int>().GetByIdAsync(id);
            if (trip is null)
                return Error.NotFound("Trip.NotFound", $"The trip with {id} was not found.");
            _unitOfWork.GetRepository<Trip, int>().Delete(trip);
            await _unitOfWork.SaveChangesAsync();
            return "Trip deleted successfully.";
        }

        public async Task<Result<PaginatedResult<TripDto>>> GetAllTripsAsync(TripQueryParams tripSearch)
        {
            var repo = _unitOfWork.GetRepository<Trip, int>();
            var spec = new TripWithCItysSpecification(tripSearch);
            var trips = await repo.GetAllAsync(spec);
            if(trips == null || !trips.Any())
                return Error.NotFound("Trips.NotFound", "No trips found matching the search criteria.");
            var DataToReturn = _mapper.Map<IEnumerable<TripDto>>(trips);
            int CountOfDataToReturn = DataToReturn.Count();
            var pagedSpec = new TripCountSpecifications(tripSearch);
            int TotalCount = await repo.CountAsync(pagedSpec);
            return new PaginatedResult<TripDto>(tripSearch.PageIndex, CountOfDataToReturn, TotalCount, DataToReturn);
        }



        public async Task<Result<string>> UpdateTripAsync(UpdateTripDto dto)
        {
            var spec = new TripWithCItysSpecification(dto.Id);
            var existingTrip = await _unitOfWork.GetRepository<Trip, int>().GetByIdAsync(spec);

            if (existingTrip is null)
                return Error.NotFound("Trip.NotFound", $"The trip with ID {dto.Id} was not found.");

            _mapper.Map(dto, existingTrip);
            if (existingTrip.bus.PlateNumber != dto.BusPlateNumber)
            {
                var newBus = await _unitOfWork.GetRepository<Bus, int>()
                    .GetByIdAsync(new BusSpecifications(dto.BusPlateNumber));

                if (newBus == null) return Error.NotFound("Bus.NotFound", "New bus not found.");
                existingTrip.busId = newBus.Id;
                existingTrip.bus = newBus;
            }
            var nameParts = dto.DriverName.Split(' ', 2); 
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

            if (existingTrip.Driver.User.FirstName != firstName || existingTrip.Driver.User.LastName != lastName)
            {
                var newDriver = await _unitOfWork.GetRepository<Driver, int>()
                    .GetByIdAsync(new DriverSpecifications(dto.DriverName));

                if (newDriver == null) return Error.NotFound("Driver.NotFound", "New driver not found.");
                existingTrip.DriverId = newDriver.UserId;
                existingTrip.Driver = newDriver;
            }

            await _unitOfWork.SaveChangesAsync();
            return "Trip updated successfully.";
        }


    }
}
