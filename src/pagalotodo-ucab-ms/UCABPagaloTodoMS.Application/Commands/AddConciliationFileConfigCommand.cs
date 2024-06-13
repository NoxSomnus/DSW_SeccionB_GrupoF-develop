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
    public class AddConciliationFileConfigCommand : IRequest<Guid>
    {
        public AddConciliationFileConfigRequest _request { get; set; }
        public AddConciliationFileConfigCommand(AddConciliationFileConfigRequest request)
        {
            _request = request;
        }
    }
}
