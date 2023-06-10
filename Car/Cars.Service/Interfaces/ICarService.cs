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
        Task<CarDTO> GetCar(Guid id);
        Task<Dictionary<Guid, CarDTO>> GetCars(CarFilter carFilter);
        Task<Guid> AddCar(CarDTO carDTO);
        Task UpdateCar(Guid id, CarDTO carDTO);
        Task BookingCar(Guid id);
        Task CancelBookingCar(Guid id);
        Task RemoveCar(Guid id);
    }
}
