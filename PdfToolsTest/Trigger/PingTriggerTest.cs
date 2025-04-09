using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfTools.Trigger;

namespace PdfToolsTest.Trigger;

[TestClass]
public class PingTriggerTest
{
    [TestMethod("Should be ping")]
    public void ConvertHtmlToPdfSuccess()
    {
        var request = new Mock<HttpRequest>();
        var route = new PingTrigger();
        var response = route.Run(request.Object);
        Assert.AreSame(typeof(OkResult), response.GetType());
    }

}