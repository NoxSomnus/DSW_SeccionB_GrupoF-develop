using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class PaymentFieldContentRequest
    {
        public Guid FieldId { get; set; }
        public string? Content { get; set; }
    }
}
