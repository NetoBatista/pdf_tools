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
    public class PdfToTextTrigger
    {
        private readonly IPdfToTextService _pdfToTextService;

        public PdfToTextTrigger(IPdfToTextService pdfToTextService)
        {
            _pdfToTextService = pdfToTextService;
        }

        [Function("PdfToText")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var request = await req.GetBody<PdfToTextRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _pdfToTextService.ExtractTextFromPdf(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new OkObjectResult(response.Data);
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
