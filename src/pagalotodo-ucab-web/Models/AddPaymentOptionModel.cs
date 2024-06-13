using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class AddPaymentOptionModel
    {
        public Guid ServiceId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string? Nombre { get; set; }
        public string? Status { get; set; }
    }
}
