using Infrastructure.HelperModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Entities
{
    public class DrivingLicense:BaseEntity<Guid>
    {
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? DateIssued { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }

        public virtual Client Client { get; set; }
    }
}
