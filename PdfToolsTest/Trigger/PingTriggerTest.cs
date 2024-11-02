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
public class PingTriggerTest
{
    [TestMethod("Should be ping")]
    public async Task ConvertHtmlToPdfSuccess()
    {
        var request =new Mock<HttpRequest>();
        var route = new PingTrigger();
        var response = route.Run(request.Object);
        Assert.AreSame(typeof (NoContentResult), response.GetType());
    }
    
}