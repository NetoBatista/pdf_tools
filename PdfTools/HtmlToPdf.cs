using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Service;

namespace PdfTools
{
    public class HtmlToPdf
    {
        private readonly IHtmlToPdfService _htmlToPdfService;

        public HtmlToPdf(IHtmlToPdfService htmlToPdfService)
        {
            _htmlToPdfService = htmlToPdfService;
        }

        [Function("HtmlToPdf")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var request = await req.GetBody<HtmlToPdfRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _htmlToPdfService.ConvertHtmlToPdf(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new FileContentResult((byte[])response.Data, "application/pdf");
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
