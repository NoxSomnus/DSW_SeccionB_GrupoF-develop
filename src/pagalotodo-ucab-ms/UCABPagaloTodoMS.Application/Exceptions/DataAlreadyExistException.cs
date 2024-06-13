using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class DataAlreadyExistException : Exception
    {
        public DataAlreadyExistException(string message) : base(message)
        {
        }

        public DataAlreadyExistException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
