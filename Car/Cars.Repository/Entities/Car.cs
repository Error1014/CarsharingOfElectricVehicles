using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cars.Repository.Entities
{
    public class Car : BaseEntity<Guid>
    {
        public Guid BrandModelId { get; set; }
        public BrandModel BrandModel { get; set; }
        public string Number { get; set; }
        public string Year { get; set; }
        public bool IsRent { get; set; }//арендована
        public bool IsRepair { get; set; }//в ремонте
        public bool IsCASCO { get; set; }
        public int Mileage { get; set; }

        public virtual IEnumerable<CarTag> CarTags { get; set; }
        public virtual IEnumerable<CarCharacteristic> CarCharacteristics { get; set; }


    }
}
