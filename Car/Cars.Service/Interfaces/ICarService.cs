using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Interfaces
{
    public interface ICarService
    {
        Task<CarInfoDTO> GetCar(Guid id);
        Task<IEnumerable<CarInfoDTO>> GetCars(PageFilter pageFilter);
        Task AddCar(CarAddUpdateDTO carDTO);
        Task UpdateCar(Guid id,CarAddUpdateDTO carDTO);
        Task BookingCar(Guid id);
        Task CancelBookingCar(Guid id);
        Task RemoveCar(Guid id);
    }
}
