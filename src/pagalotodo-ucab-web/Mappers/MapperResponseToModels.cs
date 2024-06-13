using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Mappers
{
    public class MapperResponseToModels
    {
        public List<AllServicesViewModel> MapAllServicesResponseToModel(List<AllServicesQueryResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var ViewModel = new List<AllServicesViewModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var serviceQueryResponse in response)
            {
                var serviceViewModel = new AllServicesViewModel
                {
                    ServiceId = serviceQueryResponse.ServiceId,
                    ServiceName = serviceQueryResponse.ServiceName,
                    TypeService = serviceQueryResponse.TyperService,
                    ContactNumber = serviceQueryResponse.ContactNumber,
                    ProviderUsername = serviceQueryResponse.ProviderUsername,
                    CompanyName = serviceQueryResponse.CompanyName
                };
                ViewModel.Add(serviceViewModel);
            }
            return ViewModel;
        }
        public List<PaymentRequiredFieldsModel> MapAllRequiredFieldsResponseToModel(List<PaymentRequiredFieldsResponse> response)
        {
            
            // Crear una lista para almacenar los objetos mapeados
            var ViewModel = new List<PaymentRequiredFieldsModel>();
            if (response.Count() ==0)
            {
                return ViewModel;
            }
                        
            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var optionQueryResponse in response)
            {
                var fieldViewModel = new PaymentRequiredFieldsModel
                {
                    FieldName = optionQueryResponse.FieldName,
                    PaymentOptionId = optionQueryResponse.PaymentOptionId,
                    RequiredFieldId = optionQueryResponse.RequiredFieldId,
                    isNumber = optionQueryResponse.isNumber,
                    isString = optionQueryResponse.isString,
                    Length = optionQueryResponse.Length,
                };
                ViewModel.Add(fieldViewModel);
            }
            return ViewModel;
        }

        public List<AllPaymentsByServiceViewModel> MapAllBillsInServiceResponseToModel(List<AllBillsQueryResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var ViewModel = new List<AllPaymentsByServiceViewModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var billQueryResponse in response)
            {
                var billViewModel = new AllPaymentsByServiceViewModel
                {
                    Amount = billQueryResponse.Amount,
                    Date = billQueryResponse.Date,
                    ContractNumber = billQueryResponse.ContractNumber,
                    PhoneNumber = billQueryResponse.PhoneNumber
                };
                ViewModel.Add(billViewModel);
            }
            return ViewModel;
        }
        
    }
}
