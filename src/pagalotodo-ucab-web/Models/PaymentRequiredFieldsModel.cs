using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace UCABPagaloTodoWeb.Models
{
    public class PaymentRequiredFieldsModel
    {
        public Guid PaymentOptionId { get; set; }
        public Guid RequiredFieldId { get; set; }
        [NotNull]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre del campo")]
        public string? FieldName { get; set; }

        [NotNull]
        [Display(Name = "Permite Numeros")]
        public bool? isNumber { get; set; }

        [NotNull]
        [Display(Name = "Permite Letras")]
        public bool? isString { get; set; }

        [NotNull]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Longitud")]
        public string? Length { get; set; }
        [NotNull]
        public string? Content { get; set; }
    }
}
