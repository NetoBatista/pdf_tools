using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PdfTools.Extension;

public static class HttpExtension
{
    public static async Task<T?> GetBody<T>(this HttpRequest req)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var item = JsonSerializer.Deserialize<T>(requestBody, serializerOptions);
        return item;
    }
}