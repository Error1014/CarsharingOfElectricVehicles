using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class Characteristic:BaseEntity<Guid>
    {
        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public int YearOfRelease { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
        public string WheelDrive { get; set; }
        public string Rudder { get; set; }
        public virtual Car Car { get; set; }
    }
}
