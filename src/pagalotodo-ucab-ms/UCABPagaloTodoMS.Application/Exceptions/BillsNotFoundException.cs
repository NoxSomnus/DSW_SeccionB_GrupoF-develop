using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class BillsNotFoundException : Exception
    {
        public BillsNotFoundException(string message) : base(message)
        {
        }

        public BillsNotFoundException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
