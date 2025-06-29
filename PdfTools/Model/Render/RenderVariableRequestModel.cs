using PdfTools.Extension;

namespace PdfTools.Model.Render;

public class RenderVariableRequestModel
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
