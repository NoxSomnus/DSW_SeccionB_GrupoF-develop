using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class NotFoundConfig :Exception
    {
        public NotFoundConfig(string message) : base(message)
        {
        }

        public NotFoundConfig(string message, Exception exception) : base(message, exception)
        {
        }
    }
}

