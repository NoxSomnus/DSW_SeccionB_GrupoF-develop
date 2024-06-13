using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Exceptions
{
    public class RequiredFieldsNotFoundException : Exception
    {
        public RequiredFieldsNotFoundException(string mensaje) : base(mensaje)
        {
        }
        public RequiredFieldsNotFoundException(string mensaje, Exception excepcion) : base(mensaje, excepcion)
        {
        }
    }
}
