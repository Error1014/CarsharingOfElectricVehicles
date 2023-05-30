using Chats.Repository.Context;
using Chats.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(ChatContext context)
        {
            _dbContext = context;
            Chats = new ChatRepository(context);
            Messages = new MessageRepository(context);
        }
        public IChatRepository Chats { get; private set; }

        public IMessageRepository Messages { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
