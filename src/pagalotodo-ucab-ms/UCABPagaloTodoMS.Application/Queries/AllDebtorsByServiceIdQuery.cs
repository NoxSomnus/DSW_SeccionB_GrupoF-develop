using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class AllDebtorsByServiceIdQuery : IRequest<List<UserDebtInServiceResponse>>
    {
        public Guid ServiceId { get; set; }
        public AllDebtorsByServiceIdQuery (Guid service) 
        { 
            ServiceId = service;
        }
    }
}
