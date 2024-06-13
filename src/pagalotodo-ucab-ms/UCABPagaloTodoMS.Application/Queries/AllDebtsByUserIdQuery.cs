using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class AllDebtsByUserIdQuery : IRequest<List<UserDebtInServiceResponse>>
    {
        public Guid UserId { get; set; }

        public AllDebtsByUserIdQuery(Guid user) 
        {
            UserId = user;
        }  
    }
}
