using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class MessageDTO
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public string? Text { get; set; }
        public IFormFile? File {get; set; }
        public string? FileName { get; private set; }
        public byte[]? FileData { get; private set; }
        public DateTime? DateTime { get; set; }
    }
}
