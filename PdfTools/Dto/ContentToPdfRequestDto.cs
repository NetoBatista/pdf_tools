using System.ComponentModel.DataAnnotations;
using PdfTools.Constant;
using PdfTools.Extension;

namespace PdfTools.Dto;

public class ContentToPdfRequestDto
{
    public List<ItemContentToPdfRequestDto> Content { get; set; } = [];

    public void Validate()
    {
        foreach (var item in Content)
        {
            item.Validate();
        }
    }
}

public class ItemContentToPdfRequestDto
{
    public string Value { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public int? FontSize { get; set; }
    
    public string? ImageHeight { get; set; }
    
    public string? ImageWidth { get; set; }
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(Value))
        {
            throw new ContentToPdfException("Value cannot be empty");
        }

        if (Type != TypeContentToPdfConstant.Text && Type != TypeContentToPdfConstant.Image)
        {
            throw new ContentToPdfException($"Type must be {TypeContentToPdfConstant.Text} or {TypeContentToPdfConstant.Image}");
        }
    }
}