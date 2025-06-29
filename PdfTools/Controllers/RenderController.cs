using Microsoft.AspNetCore.Mvc;
using PdfTools.Interface;
using PdfTools.Model.Render;

namespace PdfTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenderController : ControllerBase
    {
        private readonly ILogger<RenderController> _logger;
        private readonly IRenderService _renderService;

        public RenderController(ILogger<RenderController> logger, IRenderService renderService)
        {
            _logger = logger;
            _renderService = renderService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RenderRequestModel request)
        {
            var response = await _renderService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new FileContentResult((byte[])response.Data, "application/pdf");
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
