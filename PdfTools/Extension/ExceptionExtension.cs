namespace PdfTools.Extension;

public class HtmlToPdfVariableRequestException(string message) : Exception(message);

public class HtmlToPdfRequestException(string message) : Exception(message);

public class PdfToTextResponseException(string message) : Exception(message);

public class PdfToTextRequestException(string message) : Exception(message);

public class ContentToPdfException(string message) : Exception(message);