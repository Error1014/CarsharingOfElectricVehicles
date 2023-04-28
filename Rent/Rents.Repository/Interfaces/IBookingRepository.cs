using Infrastructure.Repository;
using Rents.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Interfaces
{
    public interface IBookingRepository : IRepository<Booking, Guid>
    {
    }
}
