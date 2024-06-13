using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class UserIsNotProviderException : Exception
    {
        public UserIsNotProviderException(string message): base(message)
        {
        }

        public UserIsNotProviderException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
