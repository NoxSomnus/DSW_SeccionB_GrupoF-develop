using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class DebtNotFoundException : Exception
    {
        public DebtNotFoundException(string message) : base(message)
        {
        }

        public DebtNotFoundException(string message, Exception exception) : base(message, exception)
        {
        }
    }

}
