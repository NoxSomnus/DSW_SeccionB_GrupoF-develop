using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;
using UCABPagaloTodoWeb.Mappers;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AllPaymentsByServiceController : Controller
    {
        private HttpClient _httpClient;
        private MapperResponseToModels _mapper;
        public AllPaymentsByServiceController() { 
            _httpClient = new HttpClient(); 
            _mapper = new MapperResponseToModels();
        }

        public async Task<IActionResult> AllServicesView()
        {
            var Url = "https://localhost:44339/api/servicequery/allbyproviderid?id=";
            var id = HttpContext.Session.GetString("UserId");
            var apiUrl = Url + id;
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllServicesQueryResponse>>(responseContent);

                var services = _mapper.MapAllServicesResponseToModel(Response);


                return View("~/Views/Provider/AllProviderServicesView.cshtml", services);
            }
            return View("~/Views/Provier/AccessDenied.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AllServicesView(string id)
        {
            string serviceId = id;
            var apiUrl = "https://localhost:44339/api/billquery/byserviceid=";
            var url = apiUrl + serviceId;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllBillsQueryResponse>>(responseContent);
                if (Response.Count == 0)
                {
                    return View("~/Views/Home/AccessDeniedView");
                }
                var model = _mapper.MapAllBillsInServiceResponseToModel(Response);                
                return View("~/Views/Provider/AllBillsByServicesView.cshtml", model);
            }
            return View("~/Views/Home/AccessDeniedView.cshtml");
        }

        
    }
}
