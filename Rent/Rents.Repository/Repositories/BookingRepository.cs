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

        public async Task<IEnumerable<Booking>> GetAllBookingByClient(Guid clientId)
        {
            var booking = await Set.Where(x => x.ClientId == clientId).OrderByDescending(x => x.DateTimeBeginBoocking).ToListAsync();
            return booking;
        }

        public async Task<Booking?> GetLastBooking(Guid clientId)
        {
            var booking = await Set.Where(x => x.ClientId == clientId).OrderBy(x=>x.DateTimeBeginBoocking).LastOrDefaultAsync();
            return booking;
        }
    }
}
