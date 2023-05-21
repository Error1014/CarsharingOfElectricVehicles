using Infrastructure.DTO.Rent;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Service.Interfaces
{
    public interface IRentService
    {
        Task<RentDTO> GetRent(Guid Id);
        Task<RentDTO> GetActualRent();
        Task<IEnumerable<RentDTO>> GetRents(PageFilter pageFilter);
        Task AddRent(AddRentDTO rentDTO);
        Task CancelBookingCar();
        Task StartTrip();
        Task EndTrip(decimal km);
    }
}
