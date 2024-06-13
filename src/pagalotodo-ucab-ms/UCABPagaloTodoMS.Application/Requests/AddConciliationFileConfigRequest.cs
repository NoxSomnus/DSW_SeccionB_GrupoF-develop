using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class AddConciliationFileConfigRequest
    {
        public bool IncludeDni { get; set; }
        public bool IncludeName { get; set; }
        public bool IncludeLastname { get; set; }
        public bool IncludeUsername { get; set; }
        public bool IncludeEmail { get; set; }
        public bool IncludePhoneNumber { get; set; }

        public bool IncludeAmount { get; set; }
        public bool IncludeBillDate { get; set; }
        public bool IncludeContractnumber { get; set; }

        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
