using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Reflection;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AddConciliacionFileConfigController : Controller
    {
        private HttpClient _httpClient;

        public AddConciliacionFileConfigController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult ConciliationConfigView(Guid ProviderId, Guid ServiceId)
        {
            var model = new AddConciliationFileConfigurationViewModel
            {
                ProviderId = ProviderId,
                ServiceId = ServiceId,
                includeDni = false,
                includeName = false,
                includeLastname = false,
                includeUsername = false,
                includeEmail = false,
                includePhoneNumber = false,
                includeAmount = false,
                includeBillDate = false,
                includeContractnumber = false
            };

            return View("~/Views/Administration/AddConciliacionConfigView.cshtml", model);
        }
        public async Task<IActionResult> SaveConciliationConfigView(AddConciliationFileConfigurationViewModel request)
        {
            var apiUrl = "https://localhost:44339/api/conciliation/addconciliationfileconfig";

            var jsonBody = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<Guid>(responseContent);
                return RedirectToAction("Index", "Home");

            }
            return View("~/Views/Administration/AllServicesView.cshtml");

        }


    }
}
