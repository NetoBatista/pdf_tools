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

        if (!IsBase64FileSizeUnderLimit(File))
        {
            throw new PdfToTextRequestException("File size greater than 50MB"); 
        }
        
        var pdfBytes = Convert.FromBase64String(File);

        if (pdfBytes.Length < 5)
        {
            throw new PdfToTextRequestException("File is not a valid PDF");
        }

        var header = Encoding.ASCII.GetString(pdfBytes, 0, 4).Trim();
        var footer = Encoding.ASCII.GetString(pdfBytes, pdfBytes.Length - 5, 5).Trim();

        var isValid =  header.ToUpper().Contains("PDF") && footer.ToUpper().Contains("EOF");
        if (!isValid)
        {
            throw new PdfToTextRequestException("File is not a valid PDF");
        }
    }
    
    private bool IsBase64FileSizeUnderLimit(string base64String, int maxFileSizeInMB = 50)
    {
        long maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;
        long fileSizeInBytes = (long)(base64String.Length * 3 / 4);
        return fileSizeInBytes <= maxFileSizeInBytes;
    }
}