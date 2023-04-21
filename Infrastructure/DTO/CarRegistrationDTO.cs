using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class CarRegistrationDTO
    {
        public Guid BrandModelId { get; set; }
        public string Number { get; set; }
        public string Year { get; set; }
        public bool IsCASCO { get; set; }
        public int Mileage { get; set; }
    }
}
