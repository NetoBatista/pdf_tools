using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfTools;
using PdfTools.Dto;
using PdfTools.Interface;
using PdfTools.Model;
using PdfTools.Service;

namespace PdfToolsTest.Route;

[TestClass]
public class HtmlToPdfTest
{
    public HtmlToPdf CreateRoute(IHtmlToPdfService htmlToPdfService)
    {
        return new HtmlToPdf(htmlToPdfService);
    }
    
    [TestMethod("Should be convert html to pdf")]
    public async Task ConvertHtmlToPdfSuccess()
    {
        var request =new Mock<HttpRequest>();
        request.Setup(x => x.Body)
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<IHtmlToPdfService>();
        htmlToPdfServiceMock.Setup(x => x.ConvertHtmlToPdf(It.IsAny<HtmlToPdfRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.OK, Encoding.Unicode.GetBytes("abc")));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof (FileContentResult), response.GetType());
    }
    
    [TestMethod("Should not be convert html to pdf - bad request service")]
    public async Task ConvertHtmlToPdfBadRequestService()
    {
        var request =new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<IHtmlToPdfService>();
        htmlToPdfServiceMock.Setup(x => x.ConvertHtmlToPdf(It.IsAny<HtmlToPdfRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, "Error"));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof (BadRequestObjectResult), response.GetType());
    }
}