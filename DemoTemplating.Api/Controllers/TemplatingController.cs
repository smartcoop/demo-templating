using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;

namespace DemoTemplating.Api.Controllers {
    [ApiController]
    public class TemplatingController : ControllerBase {

        private static readonly string TemplateFolder = "./Templates";
        private static readonly string JsonServerUrl = "http://json-server";

        [HttpGet]
        [Route("templating/{htmlFileName}/{jsonFileName}")]
        public ContentResult Get(string htmlFileName, string jsonFileName) {

            string remoteJsonUrl = $"{JsonServerUrl}/{jsonFileName}.json";
            string json;

            using (var response = new HttpClient().GetAsync(remoteJsonUrl).Result) {
                if (!response.IsSuccessStatusCode) {
                    return new ContentResult {
                        ContentType = "text/html",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }

                json = response.Content.ReadAsStringAsync().Result;
            }
            
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
