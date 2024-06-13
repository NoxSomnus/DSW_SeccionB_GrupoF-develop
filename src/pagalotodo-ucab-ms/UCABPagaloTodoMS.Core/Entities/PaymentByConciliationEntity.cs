using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class PaymentByConciliationEntity : BaseEntity
    {
        public double? Debt { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }
    }
}
