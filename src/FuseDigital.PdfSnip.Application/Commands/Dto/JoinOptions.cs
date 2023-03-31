using CommandLine;

namespace FuseDigital.PdfSnip.Commands.Dto;

[Verb("join", HelpText = "Join multiple pdf documents in specific folder into a single document")]
public class JoinOptions : PdfCommandOptions
{
    [Value(0, MetaName = "input-directory-path", Required = true, Default = ".", HelpText = "The path of the input directory")]
    public string InputDirectoryPath { get; set; }

    [Value(1, MetaName = "output-document", Required = true, HelpText = "The path of the PDF document")]
    public string OutputPdfDocumentPath { get; set; }

    public override Type GetCommandType()
    {
        return typeof(JoinCommand);
    }
}