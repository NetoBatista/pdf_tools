using PdfTools.Extension;

namespace PdfTools.Dto;

public class HtmlToPdfRequestDto
{
    public string Content { get; set; } = string.Empty;
    
    public List<HtmlToPdfVariableRequestDto> Variables { get; set; } = [];

    public void Validate()
    {
        if (string.IsNullOrEmpty(Content))
        {
            throw new HtmlToPdfRequestException("Content is required");
        }
    }
}
