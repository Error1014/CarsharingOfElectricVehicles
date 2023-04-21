using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class Characteristic:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? UOM { get; set; }//единицы измерения
        public virtual IEnumerable<CarCharacteristic> CarCharacteristics { get; set; }
    }
}
