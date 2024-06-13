namespace UCABPagaloTodoWeb.Models
{
    public class AddPaymentModel
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }
        public List<PaymentRequiredFieldsModel>? RequiredFields { get; set; }
    }
}
