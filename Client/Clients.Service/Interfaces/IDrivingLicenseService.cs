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
        Task<Dictionary<Guid, DrivingLicenseDTO>> GetDrivingLicenses(DefoltFilter pageFilter);
        Task<Guid> AddDrivingLicense(DrivingLicenseDTO drivingLicenseDTO);
        Task UpdateDrivingLicense(Guid id, DrivingLicenseDTO drivingLicenseDTO);
        Task RemoveDrivingLicense(Guid id);
    }
}
