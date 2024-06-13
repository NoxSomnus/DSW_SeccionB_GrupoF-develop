using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class UserDebtInServiceQuery : IRequest<UserDebtInServiceResponse>
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }

        public UserDebtInServiceQuery(Guid userId, Guid serviceId)
        {
            UserId = userId;
            ServiceId = serviceId;
        }
    }
}
