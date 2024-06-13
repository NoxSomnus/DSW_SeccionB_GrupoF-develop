using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class UserIsNotCustomerException : Exception
    {
        public UserIsNotCustomerException(string message) : base(message)
        {
        }

        public UserIsNotCustomerException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
