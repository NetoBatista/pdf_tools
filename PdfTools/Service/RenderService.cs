using iText.Html2pdf;
using Microsoft.Extensions.Logging;
using PdfTools.Dto.Render;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Model;
using System.Net;

namespace PdfTools.Service;

public class RenderService : IRenderService
{
    private readonly ILogger<RenderService> _logger;

    public RenderService(ILogger<RenderService> logger)
    {
        _logger = logger;
    }

    public ResponseBaseModel Execute(RenderRequestDto request)
    {
        try
        {
            request.Validate();

            foreach (var variable in request.Variables)
            {
                variable.Validate();
                request.Content = request.Content.Replace($"{{{variable.Name}}}", variable.Value);
            }

            var response = new MemoryStream();
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