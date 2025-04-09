using PdfTools.Dto.Render;
using PdfTools.Extension;

namespace PdfToolsTest.Dto;

[TestClass]
public class RenderVariableRequestDtoTest
{
    private const string HtmlExample =
        "<!DOCTYPEhtml><htmllang=\\\"en-US\\\"><head><metacharset=\\\"UTF-8\\\"><metaname=\\\"viewport\\\"content=\\\"width=device-width,initial-scale=1.0\\\"></head><body><h1>Hello {userName}</h1></body></html>";

    [TestMethod("Should not be create request - error variables")]
    public void CreateFailVariables()
    {
        var request = new RenderRequestDto
        {
            Content = HtmlExample,
            Variables =
            [
                new RenderVariableRequestDto()
                {
                    Name = string.Empty,
                    Value = string.Empty
                }
            ]
        };

        Assert.ThrowsException<HtmlToPdfVariableRequestException>(() => request.Variables.First().Validate());
    }
}