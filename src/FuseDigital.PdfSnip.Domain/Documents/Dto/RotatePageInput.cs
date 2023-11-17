namespace FuseDigital.PdfSnip.Documents.Dto;

public class RotatePageInput
{
    public string PdfDocumentPath { get; set; }

    public int PageNumber { get; set; }

    public int RotationDegrees { get; set; }
}