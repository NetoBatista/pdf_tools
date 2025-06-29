using PdfTools.Model;
using PdfTools.Model.Render;

namespace PdfTools.Interface;

public interface IRenderService
{
    Task<ResponseBaseModel> Execute(RenderRequestModel request);
}
