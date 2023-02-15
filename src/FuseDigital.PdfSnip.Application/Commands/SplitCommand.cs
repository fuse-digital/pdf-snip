using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents;
using FuseDigital.PdfSnip.Documents.Dto;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace FuseDigital.PdfSnip.Commands;

public class SplitCommand : PdfCommandAsync, ITransientDependency
{
    private readonly IDocumentDomainService _documentDomainService;

    public SplitCommand(IDocumentDomainService documentDomainService)
    {
        _documentDomainService = documentDomainService;
    }

    public override async Task ExecuteAsync(IPdfCommandOptions options)
    {
        await base.ExecuteAsync(options);
        var input = ObjectMapper.Map<SplitOptions, SplitInput>((SplitOptions)options);
        var output = await _documentDomainService.SplitAsync(input);
        Logger.LogInformation("The {PdfDocument} was split into the following {Documents}", input.PdfDocumentPath, output);
    }
}