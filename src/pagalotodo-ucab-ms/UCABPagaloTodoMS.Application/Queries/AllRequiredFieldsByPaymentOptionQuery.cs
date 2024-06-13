using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class AllRequiredFieldsByPaymentOptionQuery : IRequest<List<PaymentRequiredFieldsResponse>>
    {
        public Guid PaymentOptionId { get; set; }
        public AllRequiredFieldsByPaymentOptionQuery(Guid optionId)
        {
            PaymentOptionId = optionId;
        }
    }
}
