using Clients.Repository.Entities;
using Clients.Repository.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Repositories
{
    public class DrivingLicenseRepository : Repository<DrivingLicense, Guid>, IDrivingLicenseRepository
    {
        public DrivingLicenseRepository(DbContext context) : base(context)
        {
        }
    }
}
