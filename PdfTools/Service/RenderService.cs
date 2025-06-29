using iText.Html2pdf;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Model;
using PdfTools.Model.Render;
using System.Net;

namespace PdfTools.Service;

public class RenderService : IRenderService
{
    private readonly ILogger<RenderService> _logger;

    public RenderService(ILogger<RenderService> logger)
    {
        _logger = logger;
    }

    public async Task<ResponseBaseModel> Execute(RenderRequestModel request)
    {
        try
        {
            await request.Validate();

            foreach (var variable in request.Variables)
            {
                variable.Validate();
                request.Content = request.Content.Replace($"{{{variable.Name}}}", variable.Value);
            }

            using var response = new MemoryStream();
            HtmlConverter.ConvertToPdf(request.Content, response);
            return new ResponseBaseModel(HttpStatusCode.OK, response.ToArray());
        }
        catch (HtmlToPdfVariableRequestException error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, error.Message);
        }
        catch (HtmlToPdfRequestException error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, error.Message);
        }
        catch (Exception error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, "An error occurred while trying to convert");
        }
    }
}
