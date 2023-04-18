using System;
using System.Collections.Generic;
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
        public DateTime? Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateRegistration { get; set; }
        public int RoleId { get; set; }
    }
}
