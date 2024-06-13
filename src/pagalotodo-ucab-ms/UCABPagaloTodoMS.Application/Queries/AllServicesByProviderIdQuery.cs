using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class AllServicesByProviderIdQuery : IRequest<List<AllServicesQueryResponse>>
    {
        public Guid ProviderId { get; set; }
        public AllServicesByProviderIdQuery (Guid providerId)
        {
            ProviderId = providerId;
        }
    }
}
