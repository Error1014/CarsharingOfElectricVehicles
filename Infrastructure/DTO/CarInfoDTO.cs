using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class CarInfoDTO
    {
        public Guid BrandModelId { get; set; }
        public string Number { get; set; }
        public int Year { get; set; }
        public bool IsRent { get; set; }//арендована
        public bool IsRepair { get; set; }//в ремонте
        public bool IsCASCO { get; set; }
        public int Mileage { get; set; }
    }
}
