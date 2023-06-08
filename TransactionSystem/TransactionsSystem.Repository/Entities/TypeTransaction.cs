using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Repository.Entities
{
    public class TypeTransaction:BaseEntity<int>
    {
        public string Name { get; set; }

        public TypeTransaction()
        {

        }
        public TypeTransaction(string name)
        {
            Name = name;
        }
    }
}
