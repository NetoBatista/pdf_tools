using System.Net;
using iText.Html2pdf;
using Microsoft.Extensions.Logging;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Model;

namespace PdfTools.Service;

public class HtmlToPdfService: IHtmlToPdfService
{
    private readonly ILogger<HtmlToPdfService> _logger;

    public HtmlToPdfService(ILogger<HtmlToPdfService> logger)
    {
        _logger = logger;
    }
    
    public ResponseBaseModel ConvertHtmlToPdf(HtmlToPdfRequestDto request)
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