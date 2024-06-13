namespace UCABPagaloTodoWeb.Models
{
    public class Registered_New_RequiredFieldsModel
    {
        public Guid PaymentOptionId { get; set; }
        public List<PaymentRequiredFieldsModel>? RegisteredFields { get; set; }
        public List<PaymentRequiredFieldsModel>? NewFields { get; set; }
    }
}
