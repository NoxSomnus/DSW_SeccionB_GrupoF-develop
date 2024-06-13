using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class UserDebtInServiceResponse
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public double? Debt { get; set; }
    }
}
