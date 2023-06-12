using Chats.Repository.Entities;
using Chats.Repository.Interfaces;
using Infrastructure.Filters;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Repositories
{
    public class MessageRepository : Repository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid chatId, DefoltFilter pageFilter)
        {
            var list = Set.Where(x => x.ChatId == chatId).OrderByDescending(x => x.DateTime).AsQueryable();
            list = list.Skip(pageFilter.Offset).Take(pageFilter.SizePage);
            return list;

        }
    }
}
