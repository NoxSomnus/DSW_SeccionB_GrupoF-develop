using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException(string message) : base(message)
        {
        }

        public ChangePasswordException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
