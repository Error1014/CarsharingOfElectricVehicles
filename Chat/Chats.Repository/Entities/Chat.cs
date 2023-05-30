using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Entities
{
    public class Chat:BaseEntity<Guid>
    {
        public Guid ClientId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
