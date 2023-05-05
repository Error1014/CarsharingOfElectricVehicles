using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Service.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDTO> GetBooking(Guid id);
        Task<IEnumerable<BookingDTO>> GetBookings();
        Task<IEnumerable<BookingDTO>> GetBookingsByClient();
        Task AddBooking(BookingDTO bookingDTO);
        Task RemoveBooking(Guid id);
        Task UpdateBooking(Guid id, BookingDTO bookingDTO);
    }
}
