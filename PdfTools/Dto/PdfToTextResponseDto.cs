namespace PdfTools.Dto;

public class PdfToTextResponseDto
{
    public PdfToTextResponseDto()
    {
        Items = [];
    }
    public List<PdfToTextItemResponseDto> Items { get; set; }
}