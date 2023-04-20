using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IConfigurationItemRepository ConfigurationItems { get; }
        int Complete();
    }
}
