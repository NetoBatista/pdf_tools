using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfTools.Trigger;
using PdfTools.Dto;
using PdfTools.Interface;
using PdfTools.Model;

namespace PdfToolsTest.Trigger;

[TestClass]
public class ContentToPdfTriggerTest
{
    public ContentToPdfTrigger CreateRoute(IContentToPdfService contentToPdfService)
    {
        return new ContentToPdfTrigger(contentToPdfService);
    }

    [TestMethod("Should be convert content to pdf")]
    public async Task ConvertContentToPdfSuccess()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var contentToPdfService = new Mock<IContentToPdfService>();
        contentToPdfService.Setup(x => x.Execute(It.IsAny<ContentToPdfRequestDto>()))
                           .Returns(new ResponseBaseModel(HttpStatusCode.OK, Encoding.Unicode.GetBytes("abc")));
        var route = CreateRoute(contentToPdfService.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(FileContentResult), response.GetType());
    }

    [TestMethod("Should not be convert content to pdf - bad request service")]
    public async Task ConvertContentToPdfBadRequestService()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var contentToPdfService = new Mock<IContentToPdfService>();
        contentToPdfService.Setup(x => x.Execute(It.IsAny<ContentToPdfRequestDto>()))
                           .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, "Error"));
        var route = CreateRoute(contentToPdfService.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(BadRequestObjectResult), response.GetType());
    }
}