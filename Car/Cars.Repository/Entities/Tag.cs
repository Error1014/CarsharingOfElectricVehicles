using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class Tag:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public virtual IEnumerable<CarTag> CarTags { get; set; }
    }
}
