using PdfTools.Extension;

namespace PdfTools.Dto.Render;

public class RenderVariableRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Value))
        {
            throw new HtmlToPdfVariableRequestException("Name and value are required");
        }
    }
}