namespace UCABPagaloTodoWeb.Models
{
    public class AddConciliationFileConfigurationViewModel
    {
        
            public bool includeDni { get; set; }
            public bool includeName { get; set; }
            public bool includeLastname { get; set; }
            public bool includeUsername { get; set; }
            public bool includeEmail { get; set; }
            public bool includePhoneNumber { get; set; }

            public bool includeAmount { get; set; }
            public bool includeBillDate { get; set; }
            public bool includeContractnumber { get; set; }
            public Guid ProviderId { get; set; }
            public Guid ServiceId { get; set; }

        
    }
}
