using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class CarCharacteristic:BaseEntity<Guid>
    {
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
        public Guid CharacteristicId { get; set; }
        public virtual Characteristic Characteristic { get; set; }
        public string Value { get; set; }
    }
}
