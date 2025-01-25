using PdfTools.Dto;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface IContentToPdfService
{
    ResponseBaseModel Execute(ContentToPdfRequestDto request);
}