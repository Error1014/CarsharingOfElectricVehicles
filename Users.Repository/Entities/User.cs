using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Repository.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateRegistration { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
