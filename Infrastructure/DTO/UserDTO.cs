using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateRegistration { get; set; }
        public int RoleId { get; set; }
    }
}
