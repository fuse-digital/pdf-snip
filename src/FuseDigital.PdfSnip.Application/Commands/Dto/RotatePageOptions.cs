using CommandLine;

namespace FuseDigital.PdfSnip.Commands.Dto;

[Verb("rotate", HelpText = "Rotate the specified page in the pdf document by the specified degrees")]
public class RotatePageOptions : PdfCommandOptions
{
    [Value(0, MetaName = "pdf-document-path", Required = true, HelpText = "The path of the PDF document")]
    public string PdfDocumentPath { get; set; }
    
    [Value(1, MetaName = "page-number", Required = true, HelpText = "The page number that should be rotated")]
    public int PageNumber { get; set; }
    
    [Value(2, MetaName = "rotation-degrees", Required = true, Default = 180, HelpText = "The page number that should be rotated")]
    public int RotationDegrees { get; set; }
    
    public override Type GetCommandType()
    {
        return typeof(RotatePageCommand);
    }
}