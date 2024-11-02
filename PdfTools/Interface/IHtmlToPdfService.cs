using PdfTools.Dto;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface IHtmlToPdfService
{
    ResponseBaseModel ConvertHtmlToPdf(HtmlToPdfRequestDto request);
}