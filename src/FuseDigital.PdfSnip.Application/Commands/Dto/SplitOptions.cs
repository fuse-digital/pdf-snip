using CommandLine;

namespace FuseDigital.PdfSnip.Commands.Dto;

[Verb("split", HelpText = "Split the pages of the pdf document into separate documents.")]
public class SplitOptions : PdfCommandOptions
{
    [Value(0, MetaName = "pdf-document-path", Required = true, HelpText = "The path of the PDF document")]
    public string PdfDocumentPath { get; set; }

    [Value(1, MetaName = "output-directory-path", Required = true, Default = ".", HelpText = "The path of the output directory")]
    public string OutputDirectoryPath { get; set; }

    public override Type GetCommandType()
    {
        return typeof(SplitCommand);
    }
}