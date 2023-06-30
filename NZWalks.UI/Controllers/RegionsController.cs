using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Net.Http.Headers;
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = HttpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7107/api/Region/{id.ToString()}");

            if(response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = HttpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7107/api/Region/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if(response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {
                var client = HttpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7081/api/regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
