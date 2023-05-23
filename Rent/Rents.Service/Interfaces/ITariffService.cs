using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Service.Interfaces
{
    public interface ITariffService
    {
        Task<TariffDTO> GetTariff(Guid Id);
        Task<Dictionary<Guid,TariffDTO>> GetTariffs();
        Task AddTariff(TariffDTO tariffDTO);
        Task RemoveTariff(Guid Id);
        Task UpdateTarif(Guid Id,TariffDTO tariffDTO);
    }
}
