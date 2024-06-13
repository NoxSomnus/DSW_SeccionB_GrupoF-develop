using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class AddPaymentRequiredFieldsRequest
    {
        public Guid PaymentOptionId { get; set; }
        public List<RequiredFieldsRequest> RequiredFields { get; set; }
}
}
