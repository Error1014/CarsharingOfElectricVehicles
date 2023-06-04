using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ChatDTO
    {
        public Guid ClientId { get; set; }
        public string? SurnameClient { get; set; }
        public string? NameClient { get; set; }
        public string? PatronymicClient { get; set; }
    }
}
