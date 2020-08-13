using RazorEngine;
using System;
using RazorEngine.Templating;

namespace DemoTemplating.RendererLib {
    public class TemplatingService {

        public static string Render(string html, string json) {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return Engine.Razor.RunCompile(html, Guid.NewGuid().ToString(), null, data);
        }
    }
}
