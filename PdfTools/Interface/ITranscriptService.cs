using PdfTools.Dto.Transcript;
using PdfTools.Model;

namespace PdfTools.Interface;

public interface ITranscriptService
{
    ResponseBaseModel Execute(TranscriptRequestDto request);
}