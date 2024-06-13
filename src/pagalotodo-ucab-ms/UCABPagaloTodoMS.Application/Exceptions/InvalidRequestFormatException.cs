using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class InvalidRequestFormatException : Exception
    {
        public InvalidRequestFormatException(string message) : base(message) 
        {
        }

        public InvalidRequestFormatException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
