using AutoMapper;
using Domain.Contracts;
using Domain.Modules.BusModule;
using Domain.Modules.TripModule;
using Services.Specifications.BusSpecifications;
using Services.Specifications.TripSpecifications;
using ServicesAbstraction;
using Shared;
using Shared.CommonResult;
using Shared.Dtos.TripDto;
using Shared.DTOs.BusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BusService : IBusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        public async Task<Result<string>> CreateAsync(CreateBusDto dto)
        {
            var spec = new BusSpecifications(dto.PlateNumber);
            var existingBus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(spec);
            if (existingBus != null)
            {
                return Error.Conflict("Bus Already Exists", $"A bus with the plate number {dto.PlateNumber} already exists.");
            }
            var bus = _mapper.Map<Bus>(dto);
            await _unitOfWork.GetRepository<Bus, int>().AddAsync(bus);
            await _unitOfWork.SaveChangesAsync();
            return "Bus Created Successfully";
        }


        public async Task<Result<string>> DeleteAsync(string PlateNumber)
        {
            var spec = new BusSpecifications(PlateNumber);
            var bus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(spec);

            if (bus is null)
                return Error.NotFound("Bus Not Found", $"No bus found with PlateNumber {PlateNumber}.");
            _unitOfWork.GetRepository<Bus, int>().Delete(bus);
            await _unitOfWork.SaveChangesAsync();
            return "Bus Deleted Successfully";
        }

        public async Task<Result<PaginatedResult<BusDto>>> GetAllAsync(BusQueryParams busQuery)
        {
            var repo = _unitOfWork.GetRepository<Bus, int>();
            var spec = new BusSpecifications(busQuery);
            var buses = await repo.GetAllAsync(spec);
            var DataToReturn = _mapper.Map<IEnumerable<BusDto>>(buses);
            int CountOfDataToReturn = DataToReturn.Count();
            var pagedSpec = new BusCountSpecifications(busQuery);
            int TotalCount = await repo.CountAsync(pagedSpec);
            return new PaginatedResult<BusDto>(busQuery.PageIndex, CountOfDataToReturn, TotalCount, DataToReturn);

        }

        public async Task<Result<PaginatedResult<TripDto>>> GetBusTripsAsync(string PlateNumber, TripQueryParams tripQuery)
        {
            var spec = new BusTripsSpecifications(PlateNumber);
            var bus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(spec);
            if (bus is null)
                return Error.NotFound("Bus.NotFound", $"No bus found with PlateNumber {PlateNumber}.");
            var tripSpec = new TripWithCItysSpecification(tripQuery, bus.Id);
            var DataToReturnWithSpec = await _unitOfWork.GetRepository<Trip, int>().GetAllAsync(tripSpec);
            var DataToReturn = _mapper.Map<IEnumerable<TripDto>>(DataToReturnWithSpec);
            var pagedSpec = new TripCountSpecifications(tripQuery, bus.Id);
            int TotalCount = await _unitOfWork.GetRepository<Trip, int>().CountAsync(pagedSpec);
            return new PaginatedResult<TripDto>(tripQuery.PageIndex, DataToReturnWithSpec.Count(), TotalCount, DataToReturn);




        }

        public async Task<Result<BusDto>> GetByPlateNumberAsync(string PlateNumber)
        {
            var spec = new BusSpecifications(PlateNumber);
            var bus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(spec);
            if (bus is null)
                return Error.NotFound("Bus Not Found", $"No bus found with PlateNumber {PlateNumber}.");
            return _mapper.Map<BusDto>(bus);
        }

        public async Task<Result<BusDto>> UpdateAsync(UpdateBusDto dto)
        {
            var spec = new BusSpecifications(dto.PlateNumber);
            var existingBus = await _unitOfWork.GetRepository<Bus, int>().GetByIdAsync(spec);
            if (existingBus is null)
                return Error.NotFound("Bus Not Found", $"No bus found with PlateNumber {dto.PlateNumber}.");
            _mapper.Map(dto, existingBus);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BusDto>(existingBus);
        }


    }
}
