using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Models
{
    public class ConciliacionFileConfiguration
    {
        // datos de user
        public bool IncludeDni { get; set; } = false;
        public bool IncludeName { get; set; } = false;
        public bool IncludeLastname { get; set; } = false;
        public bool IncludeUsername { get; set; } = false;
        public bool IncludeEmail { get; set; } = false;
        public bool IncludePhoneNumber { get; set; } = false;
        //datos de bill
        public bool IncludeAmount { get; set; } = false;
        public bool IncludeBillDate { get; set; } = false;
        public bool IncludeContractnumber { get; set; } = false;

    }
}
