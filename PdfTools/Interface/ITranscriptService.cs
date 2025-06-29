using PdfTools.Model;
using PdfTools.Model.Transcript;

namespace PdfTools.Interface;

public interface ITranscriptService
{
    Task<ResponseBaseModel> Execute(TranscriptRequestModel request);
}
