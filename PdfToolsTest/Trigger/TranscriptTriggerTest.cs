using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfTools.Dto.Transcript;
using PdfTools.Interface;
using PdfTools.Model;
using PdfTools.Trigger;
using System.Net;
using System.Text;

namespace PdfToolsTest.Trigger;

[TestClass]
public class TranscriptTriggerTest
{
    public TranscriptTrigger CreateRoute(ITranscriptService pdfToTextService)
    {
        return new TranscriptTrigger(pdfToTextService);
    }

    [TestMethod("Should be convert pdf to text")]
    public async Task ConvertPdfToText()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var pdfToTextService = new Mock<ITranscriptService>();
        pdfToTextService.Setup(x => x.Execute(It.IsAny<TranscriptRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.OK, Encoding.Unicode.GetBytes("abc")));
        var route = CreateRoute(pdfToTextService.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(OkObjectResult), response.GetType());
    }

    [TestMethod("Should not be convert pdf to text - bad request service")]
    public async Task ConvertPdfToTextBadRequestService()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<ITranscriptService>();
        htmlToPdfServiceMock.Setup(x => x.Execute(It.IsAny<TranscriptRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, "Error"));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(BadRequestObjectResult), response.GetType());
    }
}