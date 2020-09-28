using RazorEngine;
using System;
using RazorEngine.Templating;
using System.Security.Cryptography;
using System.Text;

namespace DemoTemplating.RendererLib {
    public class TemplatingService {

        public static string Render(string html, string json) {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return Engine.Razor.RunCompile(html, GetMd5Hash(html), null, data);
        }

        /// <summary>
        /// Get the MD5 hash of a string
        /// </summary>
        /// <param name="input">String from which to get the MD5 hash</param>
        /// <returns></returns>
        private static string GetMd5Hash(string input) {
            var md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash) {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
