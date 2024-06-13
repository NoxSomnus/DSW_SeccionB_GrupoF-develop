using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
	public class BillEntity : BaseEntity
	{
        //public string? ContractNumber { get; set; }
        //public string? PhoneNumber { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }
        public bool? IsConciliated { get; set; }
        public bool? IsApproved { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }
        public Guid PaymentOptionId { get; set; }
        public PaymentOptionEntity? PaymentOption { get; set; }
        public ICollection<PaymentDetailsEntity>? PaymentDetails { get; set; }
        
        public double? GetAmount()
        {
            return Amount;
        }
        public void SetAmount(double? amount)
        {
            Amount = amount;
        }
        public DateTime GetDate()
        {
            return Date;
        }
        public void SetDate(DateTime date)
        {
            Date = date;
        }
        public bool? GetIsConciliated()
        {
            return IsConciliated;
        }
        public void SetIsConciliated(bool? isConciliated)
        {
            IsConciliated = isConciliated;
        }
        public bool? GetIsApproved()
        {
            return IsApproved;
        }
        public void SetIsApproved(bool? isApproved)
        {
            IsApproved = isApproved;
        }
        public Guid GetUserId()
        {
            return UserId;
        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public UserEntity? GetUser()
        {
            return User;
        }

        public void SetUser(UserEntity? user)
        {
            User = user;
        }

        public Guid GetServiceId()
        {
            return ServiceId;
        }

        public void SetServiceId(Guid serviceId)
        {
            ServiceId = serviceId;
        }

        public ServiceEntity? GetService()
        {
            return Service;
        }

        public void SetService(ServiceEntity? service)
        {
            Service = service;
        }

        public Guid GetPaymentOptionId()
        {
            return PaymentOptionId;
        }

        public void SetPaymentOptionId(Guid paymentOptionId)
        {
            PaymentOptionId = paymentOptionId;
        }

        public PaymentOptionEntity? GetPaymentOption()
        {
            return PaymentOption;
        }

        public void SetPaymentOption(PaymentOptionEntity? paymentOption)
        {
            PaymentOption = paymentOption;
        }

        public ICollection<PaymentDetailsEntity>? GetPaymentDetails()
        {
            return PaymentDetails;
        }

        public void SetPaymentDetails(ICollection<PaymentDetailsEntity>? paymentDetails)
        {
            PaymentDetails = paymentDetails;
        }

    }
}
