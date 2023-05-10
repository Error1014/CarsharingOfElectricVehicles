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
        Task UpdateCar(Guid Id,CarAddUpdateDTO carDTO);
        Task UpdateCarRent(Guid Id,bool isRent);
        Task RemoveCar(Guid Id);
    }
}
