using System.Text;
using PdfTools.Extension;

namespace PdfTools.Dto;

public class PdfToTextRequestDto
{
    public string File { get; set; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrEmpty(File))
        {
            throw new PdfToTextRequestException("File not found"); 
        }
        
        var pdfBytes = Convert.FromBase64String(File);

        if (pdfBytes.Length < 5)
        {
            throw new PdfToTextRequestException("File is not a valid PDF");
        }

        var header = Encoding.ASCII.GetString(pdfBytes, 0, 4).Trim();
        var footer = Encoding.ASCII.GetString(pdfBytes, pdfBytes.Length - 5, 5).Trim();

        var isValid =  header == "%PDF" && footer == "%EOF";
        if (!isValid)
        {
            throw new PdfToTextRequestException("File is not a valid PDF");
        }
    }
}