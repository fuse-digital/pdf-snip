using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents;
using FuseDigital.PdfSnip.Documents.Dto;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace FuseDigital.PdfSnip.Commands;

public class RotatePageCommand : PdfCommandAsync, ITransientDependency
{
    private readonly IDocumentDomainService _documentDomainService;

    public RotatePageCommand(IDocumentDomainService documentDomainService)
    {
        _documentDomainService = documentDomainService;
    }

    public override async Task ExecuteAsync(IPdfCommandOptions options)
    {
        await base.ExecuteAsync(options);
        var input = ObjectMapper.Map<RotatePageOptions, RotatePageInput>((RotatePageOptions)options);
        var output = await _documentDomainService.RotatePageAsync(input);
        Logger.LogInformation("{Results}", output);
    }
}