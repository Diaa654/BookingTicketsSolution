using Shared;
using Shared.CommonResult;
using Shared.Dtos.TripDto;
using Shared.DTOs.TripDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ITripService 
    {
        Task<Result<PaginatedResult<TripDto>>> GetAllTripsAsync(TripQueryParams tripSearch);
        Task<Result<TripDto>> GetTripByIdAsync(int id);
        Task<Result<string>> CreateTripAsync(CreateTripDto dto);
        Task<Result<string>> UpdateTripAsync(UpdateTripDto dto);
        Task<Result<string>> DeleteTripAsync(int id);


    }
}
