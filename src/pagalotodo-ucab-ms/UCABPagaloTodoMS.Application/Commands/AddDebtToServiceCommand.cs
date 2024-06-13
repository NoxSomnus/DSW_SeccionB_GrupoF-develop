using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AddDebtToServiceCommand : IRequest<Guid>
    {
        public DebtRequest _request { get; set; }
        public AddDebtToServiceCommand(DebtRequest request)
        {
            _request = request;
        }
    }
}
