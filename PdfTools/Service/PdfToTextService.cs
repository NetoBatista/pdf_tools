﻿using System.Net;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.Extensions.Logging;
using PdfTools.Dto;
using PdfTools.Extension;
using PdfTools.Interface;
using PdfTools.Model;

namespace PdfTools.Service;

public class PdfToTextService : IPdfToTextService
{
    private readonly ILogger<PdfToTextService> _logger;

    public PdfToTextService(ILogger<PdfToTextService> logger)
    {
        _logger = logger;
    }

    public ResponseBaseModel Execute(PdfToTextRequestDto request)
    {
        try
        {
            request.Validate();
            
            var pdfBytes = Convert.FromBase64String(request.File);
            var pdfReader = new PdfReader(new System.IO.MemoryStream(pdfBytes));
            var pdfDocument = new PdfDocument(pdfReader);
            var textExtractionStrategy = new SimpleTextExtractionStrategy();

            var response = new PdfToTextResponseDto();
            for (var page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
            {
                var content = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page),textExtractionStrategy);
                if (!string.IsNullOrEmpty(content))
                {
                    content = RemoveNonPrintableCharacters(content);
                    content = RemoveBase64LikeStrings(content);
                    var item = new PdfToTextItemResponseDto(page, content);
                    response.Items.Add(item);
                }
            }

            pdfDocument.Close();
            return new ResponseBaseModel(HttpStatusCode.OK, response);
        }
        catch (PdfToTextRequestException error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, error.Message);
        }
        catch (PdfToTextResponseException error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, error.Message);
        }
        catch (Exception error)
        {
            _logger.LogError(error.Message);
            return new ResponseBaseModel(HttpStatusCode.BadRequest, "An error occurred while trying to convert");
        }
    }
    
    private string RemoveNonPrintableCharacters(string input)
    {
        // Define uma regex para manter apenas caracteres imprimíveis típicos
        return Regex.Replace(input, @"[^\u0020-\u007E]+", string.Empty);
    }
    
    private string RemoveBase64LikeStrings(string input)
    {
        // Define uma regex para detectar padrões que se parecem com base64
        return Regex.Replace(input, @"\b[A-Za-z0-9+/=]{20,}\b", string.Empty);
    }
}