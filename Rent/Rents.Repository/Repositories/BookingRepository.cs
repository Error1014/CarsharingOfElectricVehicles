using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Rents.Repository.Entities;
using Rents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(DbContext context) : base(context)
        {
        }
    }
}
