using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;
using UCABPagaloTodoWeb.Mappers;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AddPaymentController : Controller
    {
        private readonly ILogger<AddPaymentController> _logger;
        private HttpClient _httpClient;
        private MapperResponseToModels _mapper;

        public AddPaymentController(ILogger<AddPaymentController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _mapper = new MapperResponseToModels();
        }


        public async Task<IActionResult> AddDirectPaymentView(Guid _ServiceId, Guid _UserId, Guid _OptionId) //revisar si te agarra porque ahora pasas Guid y no string
        {
            var response = await _httpClient.GetAsync("https://localhost:44339/api/paymentoption/requiredfieldsbypaymentoption?request=" + _OptionId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<PaymentRequiredFieldsResponse>>(responseContent);
                var model = new AddPaymentModel();
                model.ServiceId = _ServiceId;
                model.UserId = _UserId;
                model.OptionId = _OptionId;
                var fields = _mapper.MapAllRequiredFieldsResponseToModel(Response);
                

                return View("~/Views/Administration/AllServicesView.cshtml", model);
            }
            return View("~/Views/Home/AccessDeniedView.cshtml");
        }


        /*[HttpPost]   USAR ESTE CODIGO PARA CONTACTAR CON EL NUEVO BACKEND
        public async Task<IActionResult> AddPaymentContractAction(string _ServiceId, string _UserId, string _OptionId, string _ContractNumber, double _Amount)
        {
            var apiUrl = "https://localhost:44339/api/payment/addpayment";
            var requestBody = new 
            {
                contractNumber = _ContractNumber,
                phoneNumber = "",
                amount = _Amount,
                userId = _UserId,
                serviceId = _ServiceId,
                paymentOptionId = _OptionId
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode) 
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<AddPaymentResponse>(responseContent);

                if (Response.success == true) 
                {
                    return View("~/Views/AddPayment/PaymentSucessful.cshtml");
                }
            }

            return View("~/Views/AddPayment/PaymentFailed.cshtml");
        }*/

        public async Task<IActionResult> AddPaymentPhoneAction(string _ServiceId, string _UserId, string _OptionId, string _PhoneNumber, double _Amount)
        {
            var apiUrl = "https://localhost:44339/api/payment/addpayment";
            var requestBody = new
            {
                contractNumber = "",
                phoneNumber = _PhoneNumber,
                amount = _Amount,
                userId = _UserId,
                serviceId = _ServiceId,
                paymentOptionId = _OptionId
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<AddPaymentResponse>(responseContent);

                if (Response.success == true)
                {
                    return View("~/Views/AddPayment/PaymentSucessful.cshtml");
                }
            }

            return View("~/Views/AddPayment/PaymentFailed.cshtml");
        }
    }
}
