using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

namespace Presentation.Controllers
{
   
    [Authorize(Roles = "Admin")]
    public class TripController (IServiceManger serviceManger): ApiBaseController
    {
       

        // POST: api/trip
        [HttpPost]
        public async Task<ActionResult<string>> CreateTrip([FromBody] CreateTripDto dto)
        {
            var res=await serviceManger.tripService.CreateTripAsync(dto);
            return HandleResult(res);
        }

        // GET: api/trip/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TripDto>> GetTripById(int id)
        {
            var trip = await serviceManger.tripService.GetTripByIdAsync(id);
            return HandleResult(trip);
        }

        // GET: api/trip
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<TripDto>>> GetAllTrips([FromQuery] TripQueryParams tripSearch)
        {
            var trips = await serviceManger.tripService.GetAllTripsAsync(tripSearch);
            return HandleResult(trips);
        }

        // DELETE: api/trip/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteTrip(int id)
        {
           var res= await serviceManger.tripService.DeleteTripAsync(id);
            
            return HandleResult(res);
        }

        // PUT: api/trip
        [HttpPut]
        public async Task<ActionResult<string>> UpdateTrip([FromBody] UpdateTripDto dto)
        {
            var res=await serviceManger.tripService.UpdateTripAsync(dto);
            return HandleResult(res);
        }

 

        

       
    }
}
