using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;

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
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var request = await req.GetBody<PdfToTextRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _pdfToTextService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new OkObjectResult(response.Data);
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
