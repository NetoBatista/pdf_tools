using PdfTools.Dto;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface IHtmlToPdfService
{
    ResponseBaseModel Execute(HtmlToPdfRequestDto request);
}