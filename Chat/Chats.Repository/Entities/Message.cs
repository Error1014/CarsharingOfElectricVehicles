using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Entities
{
    public class Message:BaseEntity<Guid>
    {
        public Guid SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public Guid ChatId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
