using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Interfaces
{
    public interface IDrivingLicenseService
    {
        Task<DrivingLicenseDTO> GetDrivingLicense(Guid id);
        Task<IEnumerable<DrivingLicenseDTO>> GetDrivingLicenses(PageFilter pageFilter);
        Task AddDrivingLicense(DrivingLicenseDTO drivingLicenseDTO);
        Task UpdateDrivingLicense(Guid id, DrivingLicenseDTO drivingLicenseDTO);
        Task RemoveDrivingLicense(Guid id);
    }
}
