using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class MyBaseException:Exception
    {
        public HttpStatusCode CodeException { get; set; }
        public MyBaseException(string message, HttpStatusCode code) : base(message)
        {
            CodeException = code;
        }
    }
}
