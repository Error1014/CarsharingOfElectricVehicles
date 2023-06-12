using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Interfaces
{
    public interface IPassportService
    {
        Task<PassportDTO> GetPassport(Guid id);
        Task<Dictionary<Guid,PassportDTO>> GetPassports(DefoltFilter pageFilter);
        Task<Guid> AddPassport(PassportDTO passportDTO);
        Task UpdatePassport(Guid id,PassportDTO passportDTO);
        Task RemovePassport(Guid id);
    }
}
