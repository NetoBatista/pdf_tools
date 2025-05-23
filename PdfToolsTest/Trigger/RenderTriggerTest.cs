﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfTools.Dto.Render;
using PdfTools.Interface;
using PdfTools.Model;
using PdfTools.Trigger;
using System.Net;
using System.Text;

namespace PdfToolsTest.Trigger;

[TestClass]
public class RenderTriggerTest
{
    public RenderTrigger CreateRoute(IRenderService htmlToPdfService)
    {
        return new RenderTrigger(htmlToPdfService);
    }

    [TestMethod("Should be convert html to pdf")]
    public async Task ConvertHtmlToPdfSuccess()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<IRenderService>();
        htmlToPdfServiceMock.Setup(x => x.Execute(It.IsAny<RenderRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.OK, Encoding.Unicode.GetBytes("abc")));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(FileContentResult), response.GetType());
    }

    [TestMethod("Should not be convert html to pdf - bad request service")]
    public async Task ConvertHtmlToPdfBadRequestService()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Body)
               .Returns(new MemoryStream(Encoding.UTF8.GetBytes("{}")));
        var htmlToPdfServiceMock = new Mock<IRenderService>();
        htmlToPdfServiceMock.Setup(x => x.Execute(It.IsAny<RenderRequestDto>()))
                            .Returns(new ResponseBaseModel(HttpStatusCode.BadRequest, "Error"));
        var route = CreateRoute(htmlToPdfServiceMock.Object);
        var response = await route.Run(request.Object);
        Assert.AreSame(typeof(BadRequestObjectResult), response.GetType());
    }
}