using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using PdfTools.Dto.Render;
using PdfTools.Extension;
using PdfTools.Interface;

namespace PdfTools.Trigger
{
    public class RenderTrigger
    {
        private readonly IRenderService _renderService;

        public RenderTrigger(IRenderService renderService)
        {
            _renderService = renderService;
        }

        [Function("Render")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var request = await req.GetBody<RenderRequestDto>();
            if (request == null)
            {
                return new BadRequestResult();
            }
            var response = _renderService.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new FileContentResult((byte[])response.Data, "application/pdf");
            }

            return new BadRequestObjectResult(response.Data);
        }
    }
}
