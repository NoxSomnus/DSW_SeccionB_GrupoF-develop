using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AddPaymentRequiredFieldsCommand : IRequest<AddPaymentRequiredFieldsResponse>
    {
        public AddPaymentRequiredFieldsRequest _request { get; set; }
        public AddPaymentRequiredFieldsCommand(AddPaymentRequiredFieldsRequest request)
        {
            _request = request;
        }
    }
}
