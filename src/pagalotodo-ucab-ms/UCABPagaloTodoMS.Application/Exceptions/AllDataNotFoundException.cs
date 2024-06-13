using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class AllDataNotFoundException : Exception
    {
        public AllDataNotFoundException(string message) : base(message)
        {
        }

        public AllDataNotFoundException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
