using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.Rent
{
    public class AddRentDTO
    {
        public Guid CarId { get; set; }
        public Guid TariffId { get; set; }
    }
}
