using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PdfTools
{
    public class HtmlToPdf
    {
        private readonly ILogger<HtmlToPdf> _logger;

        public HtmlToPdf(ILogger<HtmlToPdf> logger)
        {
            _logger = logger;
        }

        [Function("HtmlToPdf")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
