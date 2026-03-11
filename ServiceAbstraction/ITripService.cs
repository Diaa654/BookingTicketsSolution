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
        Task<PaginatedResult<TripDto>> GetAllTripsAsync(TripQueryParams tripSearch);
        Task<Result<TripDto>> GetTripByIdAsync(int id);
        Task<Result<string>> CreateTripAsync(CreateTripDto dto);
        Task<TripDto> UpdateTripAsync(UpdateTripDto dto);
        Task<bool> DeleteTripAsync(int id);


    }
}
