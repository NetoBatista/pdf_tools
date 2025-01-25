using System.Net;
using System.Text;
using iText.Html2pdf;
using Microsoft.Extensions.Logging;
using PdfTools.Constant;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Model;

namespace PdfTools.Service;

public class ContentToPdfService: IContentToPdfService
{
    private readonly ILogger<ContentToPdfService> _logger;
    private readonly IHtmlToPdfService _htmlToPdfService;

    public ContentToPdfService(ILogger<ContentToPdfService> logger, IHtmlToPdfService htmlToPdfService)
    {
        _logger = logger;
        _htmlToPdfService = htmlToPdfService;
    }
    
    public ResponseBaseModel Execute(ContentToPdfRequestDto request)
    {
        try
        {
            if (request.Content.Count == 0)
            {
                return new ResponseBaseModel(HttpStatusCode.NoContent, string.Empty);
            }
            
            request.Validate();

            List<string> elements = [];

            foreach (var content in request.Content)
            {
                StringBuilder item = new StringBuilder();
                item.Append("<div style='padding-top:8px;'>");
                if (content.Type == TypeContentToPdfConstant.Image)
                {
                    var height = content.ImageHeight ?? "32px";
                    var width = content.ImageWidth ?? "32px";
                    item.Append($"<img src='{content.Value}' height='{height}' width='{width}'/>");
                }
                else
                { 
                    var fontSize = content.FontSize ?? 12;
                    item.Append($"<span style='font-size:{fontSize}px'>{content.Value}</span>");
                }
                item.Append("</div>");
                elements.Add(item.ToString());
            }

            var template = TemplateHtml();
            template = template.Replace("{content}", string.Join(Environment.NewLine, elements));
            
            var htmlToPdfRequest = new HtmlToPdfRequestDto
            {
                Content = template
            };
            return _htmlToPdfService.Execute(htmlToPdfRequest);
        }
        catch (ContentToPdfException error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, error.Message);
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

    private string TemplateHtml()
    {
        return @"
<html>
    <head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""></head>
  <body>
    {content}
  </body>
</html>
";
    }
}