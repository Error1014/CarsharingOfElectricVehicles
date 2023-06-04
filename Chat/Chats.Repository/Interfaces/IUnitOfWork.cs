using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }

        int Complete();
    }
}
