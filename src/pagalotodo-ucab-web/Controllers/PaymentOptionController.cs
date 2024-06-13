using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;
using static System.Net.WebRequestMethods;

namespace UCABPagaloTodoWeb.Controllers
{
    public class PaymentOptionController : Controller
    {
        private HttpClient _httpClient;

        public PaymentOptionController()
        {
            _httpClient = new HttpClient();
        }
        public IActionResult AddPaymentOption(Guid serviceId)
        {
            var model = new AddPaymentOptionModel();
            model.ServiceId = serviceId;
            return View("~/Views/Administration/AddPaymentOptionView.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentOptionAction(Guid ServiceId, string Nombre, string Status)
        {
            var apiUrl = "https://localhost:44339/api/paymentoption/addpaymentoption";
            var requestBody = new
            {
                name = Nombre,
                status =  Status,
                serviceId = ServiceId
            };
        var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "La opción de pago se agregó correctamente.";
                return View("~/Views/Administration/AdminHomeView.cshtml");
            }
            return View("~/Views/Administration/AdminHomeView.cshtml");
        }

        public async Task<IActionResult> SelectPayOptionToAddRequiredFieldsView(Guid ServiceId)
        {
            var Url = "https://localhost:44339/api/paymentoption/paymentoptionbyserviceid?request=";
            var apiUrl = Url + ServiceId;
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PaymentOptionsByServiceIdResponse>>(responseContent);
                var returnModel = MapAllPaymentOptionResponseToModel(Response);
                return View("~/Views/Provider/SelectPayOptionToAddRequiredFieldsView.cshtml", returnModel);
            }
            return View("~/Views/Provider/NoPaymentOptionsOnService.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddRequiredFieldsView(Guid _PaymentOptionId)
        {
            var Url = "https://localhost:44339/api/paymentoption/requiredfieldsbypaymentoption?request=";
            var apiUrl = Url + _PaymentOptionId;
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PaymentRequiredFieldsResponse>>(responseContent);
                var returnModel = MapAllRequiredFieldsResponseToModel(Response);
                returnModel.PaymentOptionId = _PaymentOptionId;
                return View("~/Views/Provider/AddRequiredFieldsView.cshtml", returnModel);
            }
            return View("~/Views/Provider/NoPaymentOptionsOnService.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> AddRequiredFieldsAction(Guid paymentOptionId, Registered_New_RequiredFieldsModel model)
        {
            var apiUrl = "https://localhost:44339/api/paymentoption/addrequiredfields";
            if (model.NewFields == null) 
            {
                return View("~/Views/Provider/HomeProviderView.cshtml");
            }
            if (model.NewFields.Count == 0)
            {
                return View("~/Views/Provider/HomeProviderView.cshtml");
            }
            var requestBody = new
            {
                paymentOptionId = paymentOptionId,
                requiredFields = model.NewFields
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //var Response = JsonConvert.DeserializeObject<List<PaymentRequiredFieldsResponse>>(responseContent);
                //                                             Cambiar el tipo de response ^^^^^^^^
                return View("~/Views/Provider/RequiredFieldsAddedSucessfully.cshtml");
            }
            return View("~/Views/Provider/NoPaymentOptionsOnService.cshtml");
        }
        public Registered_New_RequiredFieldsModel MapAllRequiredFieldsResponseToModel(List<PaymentRequiredFieldsResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var returnModel = new Registered_New_RequiredFieldsModel();

            returnModel.RegisteredFields = new List<PaymentRequiredFieldsModel>();
            returnModel.NewFields = new List<PaymentRequiredFieldsModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var each in response)
            {
                var ViewModel = new PaymentRequiredFieldsModel
                {
                    PaymentOptionId = each.PaymentOptionId,
                    RequiredFieldId = each.RequiredFieldId,
                    FieldName = each.FieldName,
                    isNumber = each.isNumber,
                    isString = each.isString,
                    Length = each.Length
                };
                returnModel.RegisteredFields.Add(ViewModel);
            }
            return returnModel;
        }
        public List<AllPaymentOptionsModel> MapAllPaymentOptionResponseToModel(List<PaymentOptionsByServiceIdResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var ListModel = new List<AllPaymentOptionsModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var each in response)
            {
                var ViewModel = new AllPaymentOptionsModel
                {
                    PaymentOptionId = each.PaymentOptionId,
                    PaymentOptionName = each.PaymentOptionName,
                    Status = each.Status,
                };
                ListModel.Add(ViewModel);
            }
            return ListModel;
        }
    }
}
