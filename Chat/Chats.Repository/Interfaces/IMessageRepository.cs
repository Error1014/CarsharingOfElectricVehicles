using Chats.Repository.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message, Guid>
    {
    }
}
