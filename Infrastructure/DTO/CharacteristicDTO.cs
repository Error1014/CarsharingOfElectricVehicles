using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class CharacteristicDTO
    {
        public Guid CarId { get; private set; }
        public int YearOfRelease { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
        public string WheelDrive { get; set; }
        public string Rudder { get; set; }

        public CharacteristicDTO() 
        {
        }
        public CharacteristicDTO(Guid carId)
        {
            CarId = carId;
        }
    }
}
