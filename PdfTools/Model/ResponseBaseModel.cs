using System.Net;

namespace PdfTools.Model;

public class ResponseBaseModel(HttpStatusCode statusCode, object data)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public object Data { get; } = data;
}