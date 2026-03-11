using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BusController(IServiceManger serviceManger):ApiBaseController
    {
       

       
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<BusDto>>> GetAll([FromQuery] BusQueryParams query)
        {
            var result = await serviceManger.busService.GetAllAsync(query);
            return HandleResult(result);
        }

       
        [HttpGet("{PlateNumber}")]
        public async Task<ActionResult<BusDto>> GetByPlateNumber(string PlateNumber)
        {
            var bus = await serviceManger.busService.GetByPlateNumberAsync(PlateNumber);
            return HandleResult(bus);
        }

       
        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateBusDto dto)
        {
            var res=await serviceManger.busService.CreateAsync(dto);
            return HandleResult(res);
        }

       
        [HttpPut]
        public async Task<ActionResult<BusDto>> Update(UpdateBusDto dto)
        {
            var updatedBus = await serviceManger.busService.UpdateAsync(dto);
            return HandleResult(updatedBus);
        }

       
        [HttpDelete("{PlateNumber}")]
        public async Task<ActionResult<string>> Delete(string PlateNumber)
        {
            var deleted = await serviceManger.busService.DeleteAsync(PlateNumber);
           return HandleResult(deleted);
        }

       
        [HttpGet("{id}/trips")]
        public async Task<ActionResult<PaginatedResult<TripDto>>> GetBusTrips(string PlateNumber, [FromQuery] TripQueryParams tripQuery)
        {
            var trips = await serviceManger.busService.GetBusTripsAsync(PlateNumber, tripQuery);
            return HandleResult(trips);
        }
    }
}
