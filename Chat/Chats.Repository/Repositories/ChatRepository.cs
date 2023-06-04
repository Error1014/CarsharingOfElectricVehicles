using Chats.Repository.Entities;
using Chats.Repository.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Repositories
{
    public class ChatRepository : Repository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(DbContext context) : base(context)
        {
        }
    }
}
