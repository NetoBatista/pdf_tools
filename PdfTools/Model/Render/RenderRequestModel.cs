using PdfTools.Extension;

namespace PdfTools.Model.Render;

public class RenderRequestModel
{
    public string Content { get; set; } = string.Empty;

    public List<RenderVariableRequestModel> Variables { get; set; } = [];

    public Task Validate()
    {
        if (string.IsNullOrEmpty(Content))
        {
            throw new HtmlToPdfRequestException("Content is required");
        }

        return Task.CompletedTask;
    }
}
