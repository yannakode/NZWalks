using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        public IHttpClientFactory HttpClientFactory;
        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        [HttpGet]
       public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var cliente = HttpClientFactory.CreateClient();

                var httpResponseMessager = await cliente.GetAsync("https://localhost:7107/api/Region");

                httpResponseMessager.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessager.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                //ViewBag.Response = stringresponseBody;
            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = HttpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7107/api/Region"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if(response is not  null) 
            {
                return RedirectToAction("Index", "Regions");
            }
            return View(response);
        }
    }
}
