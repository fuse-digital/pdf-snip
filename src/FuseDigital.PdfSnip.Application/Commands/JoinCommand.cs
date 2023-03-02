using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents;
using FuseDigital.PdfSnip.Documents.Dto;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace FuseDigital.PdfSnip.Commands;

public class JoinCommand : PdfCommandAsync, ITransientDependency
{
    private readonly IDocumentDomainService _documentDomainService;

    public JoinCommand(IDocumentDomainService documentDomainService)
    {
        _documentDomainService = documentDomainService;
    }

    public override async Task ExecuteAsync(IPdfCommandOptions options)
    {
        await base.ExecuteAsync(options);
        var input = ObjectMapper.Map<JoinOptions, JoinInput>((JoinOptions)options);
        var output = await _documentDomainService.JoinAsync(input);
        Logger.LogInformation("The {Documents} were joined into a single {PdfDocument}", output, input.OutputPdfDocumentPath);
    }
}