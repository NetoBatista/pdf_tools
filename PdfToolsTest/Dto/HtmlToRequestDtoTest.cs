using PdfTools.Dto;
using PdfTools.Extension;

namespace PdfToolsTest.Dto;

[TestClass]
public class HtmlToRequestDtoTest
{
    private const string HtmlExample =
        "<!DOCTYPEhtml><htmllang=\\\"en-US\\\"><head><metacharset=\\\"UTF-8\\\"><metaname=\\\"viewport\\\"content=\\\"width=device-width,initial-scale=1.0\\\"></head><body><h1>Hello {userName}</h1></body></html>";
    
    [TestMethod("Should be create a valid request")]
    public void CreateSuccess()
    {
        var request = new HtmlToPdfRequestDto
        {
            Content = HtmlExample,
        };

        request.Validate();
        Assert.IsTrue(true);
    }
    
    [TestMethod("Should not be create request - error content")]
    public void CreateFailContent()
    {
        var request = new HtmlToPdfRequestDto
        {
            Content = string.Empty,
        };

        Assert.ThrowsException<HtmlToPdfRequestException>(() => request.Validate());
    }
    
    
}