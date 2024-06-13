using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class ConfigCamposConciliacionEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public string? CamposSeleccionados { get; set; }
        public ProviderEntity?  Provider { get; set; }
    }
}
