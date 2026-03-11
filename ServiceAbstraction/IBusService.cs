using Shared;
using Shared.CommonResult;
using Shared.Dtos.TripDto;
using Shared.DTOs.BusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBusService
    {
        Task<Result<PaginatedResult<BusDto>>> GetAllAsync(BusQueryParams busQuery);
        Task<Result<BusDto>> GetByPlateNumberAsync(string PlateNumber);

        Task<Result<string>> CreateAsync(CreateBusDto dto);
        Task<Result<BusDto>> UpdateAsync(UpdateBusDto dto);
        Task<Result<string>> DeleteAsync(string PlateNumber);
        Task<Result<PaginatedResult<TripDto>>> GetBusTripsAsync(string PlateNumber, TripQueryParams tripQuery);
        
        


    }

}
