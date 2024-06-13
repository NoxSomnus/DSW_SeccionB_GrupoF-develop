using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class SendFileConciliationRequest
    {
        public string? filePath { get; set; }
        public string? email { get; set; }
    }
}
