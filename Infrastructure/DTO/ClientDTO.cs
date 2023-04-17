using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal? Balance { get; set; }
    }
}
