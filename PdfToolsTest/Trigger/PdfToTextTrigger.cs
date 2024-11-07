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
public class PdfToTextTriggerTest
{
    public PdfToTextTrigger CreateRoute(IPdfToTextService pdfToTextService)
    {
        return new PdfToTextTrigger(pdfToTextService);
    }
    
    [TestMethod("Should be convert pdf to text")]
    public async Task ConvertPdfToText()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var pdfToTextService = new Mock<IPdfToTextService>();
        pdfToTextService.Setup(x => x.ExtractTextFromPdf(It.IsAny<PdfToTextRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.OK, Encoding.Unicode.GetBytes("abc")));
        var route = CreateRoute(pdfToTextService.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof (OkObjectResult), response.GetType());
    }
    
    [TestMethod("Should not be convert pdf to text - bad request service")]
    public async Task ConvertPdfToTextBadRequestService()
    {
        var request =new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<IPdfToTextService>();
        htmlToPdfServiceMock.Setup(x => x.ExtractTextFromPdf(It.IsAny<PdfToTextRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, "Error"));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof (BadRequestObjectResult), response.GetType());
    }
}