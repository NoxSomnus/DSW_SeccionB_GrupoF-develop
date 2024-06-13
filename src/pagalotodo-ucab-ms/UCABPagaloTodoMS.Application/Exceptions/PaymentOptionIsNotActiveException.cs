using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class PaymentOptionIsNotActiveException : Exception
    {
        public PaymentOptionIsNotActiveException (string message) : base(message)
        {
        }

        public PaymentOptionIsNotActiveException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
