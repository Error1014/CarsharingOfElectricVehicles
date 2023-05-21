using Infrastructure.HelperModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clients.Repository.Entities
{
    public class Client : BaseEntity<Guid>
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal? Balance { get; set; }
       
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateRegistration { get; set; }//дата регистрации в приложении
        public Guid PassportId { get; set; }
        public virtual Passport Passport { get; set; }
        public Guid DrivingLicenseId { get; set; }
        public virtual DrivingLicense DrivingLicense { get; set; }

    }
}
