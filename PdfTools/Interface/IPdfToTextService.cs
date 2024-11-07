using PdfTools.Dto;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface IPdfToTextService
{
    ResponseBaseModel ExtractTextFromPdf(PdfToTextRequestDto requestDto);
}