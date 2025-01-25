using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using PdfTools.Dto;
using PdfTools.Service;

namespace PdfToolsTest.Service;

[TestClass]
public class HtmlToPdfServiceTest
{
    private HtmlToPdfRequestDto CreateRequest()
    {
        return  new HtmlToPdfRequestDto
        {
            Content = "<html><head><metacharset=\"UTF-8\"><metaname=\"viewport\"content=\"width=device-width,initial-scale=1.0\"></head><body><h1>Hello{userName}</h1></body></html>",
            Variables =
            [
                new HtmlToPdfVariableRequestDto
                {
                    Name = "userName",
                    Value = "Jhon"
                }
            ]
        };
    }

    private HtmlToPdfService CreateService()
    {
        var loggerMock = new Mock<ILogger<HtmlToPdfService>>();
        return new HtmlToPdfService(loggerMock.Object); 
    }

    [TestMethod("Should be convert html to pdf")]
    public void ConvertHtmlToPdfSuccess()
    {
        var request = CreateRequest();
        var service = CreateService();
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }
    
    [TestMethod("Should not be convert html to pdf - error content")]
    public void ConvertHtmlToPdfErrorContent()
    {
        var request = CreateRequest();
        request.Content = string.Empty;
        var service = CreateService();
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
    }
    
    [TestMethod("Should not be convert html to pdf - error variable")]
    public void ConvertHtmlToPdfErrorVariable()
    {
        var request = CreateRequest();
        request.Variables.First().Value = string.Empty;
        var service = CreateService();
        var response = service.Execute(request);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
    }
    
}