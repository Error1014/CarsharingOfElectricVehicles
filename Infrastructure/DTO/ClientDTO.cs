using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ClientDTO
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal? Balance { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateRegistration { get; set; }//дата регистрации в приложении

        public Guid PassportId { get; set; }
        public Guid DrivingLicenseId { get; set; }


    }
}
