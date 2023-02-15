using System.Reflection;
using FuseDigital.PdfSnip.Documents.Dto;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using Volo.Abp.Domain.Services;

namespace FuseDigital.PdfSnip.Documents;

public class DocumentDomainService : DomainService, IDocumentDomainService
{
    public Task<SplitOutput> SplitAsync(SplitInput input)
    {
        var outputPath = input.OutputDirectoryPath.Equals(".")
            ? Directory.GetCurrentDirectory()
            : input.OutputDirectoryPath;

        var inputDocument = PdfReader.Open(input.PdfDocumentPath, PdfDocumentOpenMode.Import);
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
}