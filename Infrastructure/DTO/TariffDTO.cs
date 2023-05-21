using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class TariffDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
        public decimal AdditionalPrice { get; set; } //цена за минуту если время вышло
    }
}
