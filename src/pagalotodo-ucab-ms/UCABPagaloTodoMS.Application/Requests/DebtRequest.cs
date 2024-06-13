using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class DebtRequest
    {
        public string? UserName { get; set; }
        public string? Service { get; set; }
        public string? ProviderUsername { get; set; }
        public double? Amount { get; set; }
    }
}
