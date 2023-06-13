using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class ClientFilter:DefoltFilter
    {
        public string? SearchBar { get; set; }
        public DateTime? MinDateRegistration { get; set; }
        public DateTime? MaxDateRegistration { get; set; }

        public DateTime? MinDateBirthday { get; set; }
        public DateTime? MaxDateBirthday { get; set; }

        public DateTime? MinDateIssuedDrivingLicense { get; set; }
        public DateTime? MaxDateIssuedDrivingLicense { get; set; }
    }
}
