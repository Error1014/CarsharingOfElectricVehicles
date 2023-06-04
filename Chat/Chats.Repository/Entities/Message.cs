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
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public Guid SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public string? Text { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public DateTime DateTime { get; set; }

        public Message()
        {

        }
        public Message(Guid chatId, Guid senderId, Guid? recipientId, string text, string name, string fileName, byte[] fileData)
        {
            ChatId = chatId;
            SenderId = senderId;
            RecipientId = recipientId;
            Text = text;
            DateTime = DateTime.Now;
            Name = name;
            FileName = fileName;
            FileData = fileData;
        }
    }
}
