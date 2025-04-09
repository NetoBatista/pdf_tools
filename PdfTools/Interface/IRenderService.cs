using PdfTools.Dto.Render;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface IRenderService
{
    ResponseBaseModel Execute(RenderRequestDto request);
}