using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;

namespace PdfTools.Trigger
{
    public class HtmlToPdfTrigger
    {
        private readonly IHtmlToPdfService _htmlToPdfService;

        public HtmlToPdfTrigger(IHtmlToPdfService htmlToPdfService)
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
            var response = _htmlToPdfService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new FileContentResult((byte[])response.Data, "application/pdf");
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
