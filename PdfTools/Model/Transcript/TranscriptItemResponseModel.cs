using PdfTools.Extension;

namespace PdfTools.Model.Transcript;

public class TranscriptItemResponseModel
{
    public TranscriptItemResponseModel(int page, string content)
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
