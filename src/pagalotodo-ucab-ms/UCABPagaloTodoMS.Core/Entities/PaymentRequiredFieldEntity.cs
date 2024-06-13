using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class PaymentRequiredFieldEntity : BaseEntity
    {
        public string? FieldName { get; set; }
        public bool? isNumber { get; set; }
        public bool? isString { get; set; }
        public string? Length { get; set; }
        public Guid PaymentOptionId { get; set; }
        public PaymentOptionEntity? PaymentOption { get; set; }
        public ICollection<PaymentDetailsEntity>? PaymentDetails { get; set; }
    }
}
