using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Queries
{
  public class CreateConciliationFileQuery : IRequest<string>
    {
        public CreateConciliationFileRequest _request { get; set; }
        public CreateConciliationFileQuery(CreateConciliationFileRequest request)
        {
            _request = request;
        }
    }
}
