using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoTemplating.Api.Controllers {

    [ApiController]
    public class LoginController : ControllerBase {

        [HttpGet]
        [Route("form/login")]
        public ContentResult GetLoginForm() {
            return new ContentResult {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = System.IO.File.ReadAllText("./Templates/login.html")
            };
        }

        [HttpPost]
        [Route("form/login")]
        public ContentResult ValidateLoginForm([FromForm] string username, [FromForm] string password) {

            var dto = new Dto.Login() {
                Username = username,
                Password = password
            };

            // Validate the dto
            var validator = new Validator.LoginFormValidator();
            ValidationResult validationResult = validator.Validate(dto);

            // Load the login form as html to add css classes on invalid inputs
            string htmlContent = System.IO.File.ReadAllText("./Templates/login.html");
            var loginHtmlDocument = new HtmlDocument();
            loginHtmlDocument.LoadHtml(htmlContent);

            if (!validationResult.IsValid) {
                HtmlNode formNode = loginHtmlDocument.DocumentNode.SelectSingleNode("//form[@name='login']");
                foreach (var error in validationResult.Errors) {
                    HtmlNode inputNode = formNode.Descendants("input").SingleOrDefault(x => x.GetAttributeValue("name", "").Equals(error.PropertyName, StringComparison.InvariantCultureIgnoreCase));
                    inputNode?.AddClass("invalid");
                }
            }
            

            return new ContentResult {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = loginHtmlDocument.DocumentNode.OuterHtml
            };
        }

    }
}
