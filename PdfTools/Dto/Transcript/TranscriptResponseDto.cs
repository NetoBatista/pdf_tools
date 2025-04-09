namespace PdfTools.Dto.Transcript;

public class TranscriptResponseDto
{
    public TranscriptResponseDto()
    {
        Items = [];
    }
    public List<TranscriptItemResponseDto> Items { get; set; }
}