using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoTemplating.Api.Controllers {
    [ApiController]
    public class TemplatingController : ControllerBase {

        private static readonly string TemplateFolder = "./Templates";
        private static readonly string JsonServerUrl = "http://demo-templating-json-server";

        private static HttpClient Client = new HttpClient();

        [HttpGet]
        [Route("templating/{htmlFileName}/{jsonFileName}")]
        public async Task<ContentResult> Get(string htmlFileName, string jsonFileName) {

            string remoteJsonUrl = $"{JsonServerUrl}/{jsonFileName}.json";
            string json;

            var response = await Client.GetAsync(remoteJsonUrl);
            if (!response.IsSuccessStatusCode) {
                return new ContentResult {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            json = response.Content.ReadAsStringAsync().Result;

            string htmlPath = Path.Combine(TemplateFolder, $"{htmlFileName}.html");
            string html = System.IO.File.ReadAllText(htmlPath);

            return new ContentResult {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = RendererLib.TemplatingService.Render(html, json)
            };
        }
    }
}
