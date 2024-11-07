using PdfTools.Extension;

namespace PdfTools.Dto;

public class PdfToTextItemResponseDto
{
    public PdfToTextItemResponseDto(int page, string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new PdfToTextResponseException("Content is empty");
        }
        
        if (page <= 0)
        {
            throw new PdfToTextResponseException("Page is not valid");
        }
        
        Content = content;
        Page = page;
    }
    
    public int Page { get; }
    public string Content { get; }
}