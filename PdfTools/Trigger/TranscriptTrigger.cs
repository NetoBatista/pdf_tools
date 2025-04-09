using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using PdfTools.Dto.Transcript;
using PdfTools.Extension;
using PdfTools.Interface;

namespace PdfTools.Trigger
{
    public class TranscriptTrigger
    {
        private readonly ITranscriptService _transcriptService;

        public TranscriptTrigger(ITranscriptService transcriptService)
        {
            _transcriptService = transcriptService;
        }

        [Function("Transcript")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var request = await req.GetBody<TranscriptRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _transcriptService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new OkObjectResult(response.Data);
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
