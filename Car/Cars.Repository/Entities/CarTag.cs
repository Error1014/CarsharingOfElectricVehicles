using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class CarTag:BaseEntity<Guid>
    {
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
