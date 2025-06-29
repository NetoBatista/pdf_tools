using Microsoft.AspNetCore.Mvc;
using PdfTools.Interface;
using PdfTools.Model.Render;
using PdfTools.Model.Transcript;

namespace PdfTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranscriptController : ControllerBase
    {
        private readonly ILogger<TranscriptController> _logger;
        private readonly ITranscriptService _transcriptService;

        public TranscriptController(ILogger<TranscriptController> logger, ITranscriptService transcriptService)
        {
            _logger = logger;
            _transcriptService = transcriptService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TranscriptRequestModel request)
        {
            var response = await _transcriptService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new OkObjectResult(response.Data);
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
