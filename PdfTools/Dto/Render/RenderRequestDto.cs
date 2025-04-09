using PdfTools.Extension;

namespace PdfTools.Dto.Render;

public class RenderRequestDto
{
    public string Content { get; set; } = string.Empty;

    public List<RenderVariableRequestDto> Variables { get; set; } = [];

    public void Validate()
    {
        if (string.IsNullOrEmpty(Content))
        {
            throw new HtmlToPdfRequestException("Content is required");
        }
    }
}
