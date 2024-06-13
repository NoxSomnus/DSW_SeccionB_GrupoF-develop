using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class PaymentDetailsEntity : BaseEntity
    {
        public string? FieldContent { get; set; }
        public Guid BillId { get; set; }
        public BillEntity? Bill { get; set; }
        public Guid RequiredFieldId { get; set; }
        public PaymentRequiredFieldEntity? RequiredField { get; set; }
    }
}
