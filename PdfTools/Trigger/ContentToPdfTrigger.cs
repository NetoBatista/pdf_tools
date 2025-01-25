using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Service;

namespace PdfTools.Trigger
{
    public class ContentToPdfTrigger
    {
        private readonly IContentToPdfService _contentToPdfService;

        public ContentToPdfTrigger(IContentToPdfService contentToPdfService)
        {
            _contentToPdfService = contentToPdfService;
        }

        [Function("ContentToPdf")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var request = await req.GetBody<ContentToPdfRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _contentToPdfService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new NoContentResult();
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new FileContentResult((byte[])response.Data, "application/pdf");
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
