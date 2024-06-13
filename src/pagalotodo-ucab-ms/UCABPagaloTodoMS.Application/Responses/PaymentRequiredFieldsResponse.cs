using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class PaymentRequiredFieldsResponse
    {
        public Guid RequiredFieldId { get; set; }
        public string? FieldName { get; set; }
        public bool? isNumber { get; set; }
        public bool? isString { get; set; }
        public string? Length { get; set; }
        public Guid PaymentOptionId { get; set; }
    }
}
