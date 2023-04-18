using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class DublicateException : MyBaseException
    {
        public DublicateException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
