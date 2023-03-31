using FuseDigital.PdfSnip.Documents.Dto;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.IO.enums;
using Volo.Abp.Domain.Services;

namespace FuseDigital.PdfSnip.Documents;

public class DocumentDomainService : DomainService, IDocumentDomainService
{
    public Task<SplitOutput> SplitAsync(SplitInput input)
    {
        var outputPath = input.OutputDirectoryPath.Equals(".")
            ? Directory.GetCurrentDirectory()
            : input.OutputDirectoryPath;

        var inputDocument = PdfReader.Open(input.PdfDocumentPath, PdfDocumentOpenMode.Import, PdfReadAccuracy.Moderate);
        var output = new SplitOutput();
        for (var pageIndex = 0; pageIndex < inputDocument.PageCount; pageIndex++)
        {
            var outputDocument = new PdfDocument
            {
                Version = inputDocument.Version,
                Info =
                {
                    Title = inputDocument.Info.Title,
                    Creator = inputDocument.Info.Creator,
                    Author = inputDocument.Info.Author
                }
            };
            outputDocument.AddPage(inputDocument.Pages[pageIndex]);

            var outputFilename = $"{Path.GetFileNameWithoutExtension(input.PdfDocumentPath)}-{pageIndex + 1:0000}.pdf";
            outputDocument.Save(Path.Combine(outputPath, outputFilename));
            output.Add(outputFilename);
        }

        return Task.FromResult(output);
    }

    public Task<JoinOutput> JoinAsync(JoinInput input)
    {
        var inputPath = input.InputDirectoryPath.Equals(".")
            ? Directory.GetCurrentDirectory()
            : input.InputDirectoryPath;

        var files = new DirectoryInfo(inputPath)
            .GetFiles("*.pdf")
            .OrderBy(x => x.Name)
            .ToList();

        if (files.Count == 0)
        {
            return Task.FromResult(new JoinOutput());
        }

        var outputDocument = new PdfDocument();
        var output = new JoinOutput();
        foreach (var file in files)
        {
            var importDocument = PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import, PdfReadAccuracy.Moderate);
            foreach (var page in importDocument.Pages)
            {
                outputDocument.AddPage(page);
            }
            output.Add(file.Name);
        }

        outputDocument.Save(input.OutputPdfDocumentPath);
        return Task.FromResult(output);
    }
}