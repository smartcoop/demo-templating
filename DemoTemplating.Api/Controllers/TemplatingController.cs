using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;

namespace DemoTemplating.Api.Controllers {
    [ApiController]
    public class TemplatingController : ControllerBase {

        private static readonly string TemplateFolder = "./Templates";
        private static readonly string JsonServerUrl = "http://localhost:9999";

        [HttpGet]
        [Route("templating/{htmlFileName}/{jsonFileName}")]
        public IActionResult Get(string htmlFileName, string jsonFileName) {

            string remoteJsonUrl = $"{JsonServerUrl}/{jsonFileName}.json";
            string json;

            using (var response = new HttpClient().GetAsync(remoteJsonUrl).Result) {
                if (!response.IsSuccessStatusCode) {
                    return NotFound($"Failed to fetch json file from [{remoteJsonUrl}]");
                }

                json = response.Content.ReadAsStringAsync().Result;
            }
            
            string htmlPath = Path.Combine(TemplateFolder, $"{htmlFileName}.html");
            string html = System.IO.File.ReadAllText(htmlPath);

            //TODO: call DLL to template
            
            return Ok(html + json);
        }
    }
}
