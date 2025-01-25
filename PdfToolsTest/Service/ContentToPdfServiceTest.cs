using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using PdfTools.Constant;
using PdfTools.Dto;
using PdfTools.Interface;
using PdfTools.Model;
using PdfTools.Service;

namespace PdfToolsTest.Service;

[TestClass]
public class ContentToPdfServiceTest
{
    private ContentToPdfRequestDto CreateRequest()
    {
        return new ContentToPdfRequestDto
        {
            Content = new List<ItemContentToPdfRequestDto>
            {
                new ItemContentToPdfRequestDto
                {
                    Value = "https://jbsnstorage.blob.core.windows.net/public/loremipsum/loremipsum.jpg",
                    Type = TypeContentToPdfConstant.Image,
                    ImageHeight = 120,
                    ImageWidth = 700
                },
                new ItemContentToPdfRequestDto
                {
                    Value = "Text",
                    Type = TypeContentToPdfConstant.Text,
                },
            }
        };
    }

    private ContentToPdfService CreateService(IHtmlToPdfService? htmlToPdfService = null)
    {
        var loggerMock = new Mock<ILogger<ContentToPdfService>>();
        htmlToPdfService ??= new Mock<IHtmlToPdfService>().Object;
        return new ContentToPdfService(loggerMock.Object, htmlToPdfService); 
    }

    [TestMethod("Should be convert content to pdf")]
    public void ConvertHtmlToPdfSuccess()
    {
        var htmlToPdfMock = new Mock<IHtmlToPdfService>();
        htmlToPdfMock.Setup(x => x.Execute(It.IsAny<HtmlToPdfRequestDto>()))
                     .Returns(new ResponseBaseModel(HttpStatusCode.OK, string.Empty));
        var request = CreateRequest();
        var service = CreateService(htmlToPdfMock.Object);
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }
    
    [TestMethod("Should not be convert content to pdf value empty")]
    public void NotConvertHtmlToPdfValueEmpty()
    {
        var request = new ContentToPdfRequestDto
        {
            Content = new List<ItemContentToPdfRequestDto>
            {
                new ItemContentToPdfRequestDto
                {
                    
                }
            }
        };
        var service = CreateService();
        
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
    }
    
    [TestMethod("Should not be convert content to pdf invalid type")]
    public void NotConvertHtmlToPdfInvalidType()
    {
        var request = new ContentToPdfRequestDto
        {
            Content = new List<ItemContentToPdfRequestDto>
            {
                new ItemContentToPdfRequestDto
                {
                    Value = "text"
                }
            }
        };
        var service = CreateService();
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
    }
    
    [TestMethod("Should not be convert content to pdf error convert")]
    public void NotConvertHtmlToPdfSuccessErrorConvert()
    {
        var htmlToPdfMock = new Mock<IHtmlToPdfService>();
        htmlToPdfMock.Setup(x => x.Execute(It.IsAny<HtmlToPdfRequestDto>()))
            .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, string.Empty));
        var request = CreateRequest();
        var service = CreateService(htmlToPdfMock.Object);
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
    }
}