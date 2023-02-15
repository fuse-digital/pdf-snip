using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Application;
using Volo.Abp.Application.Services;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip;

[DependsOn(
    typeof(PdfSnipDomainModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule))
]
public class PdfSnipApplicationModule : AbpModule
{
}

public class PdfSnipApplicationService : ApplicationService
{
    private PdfSnipOptions Settings { get; }

    public PdfSnipApplicationService(IOptions<PdfSnipOptions> options)
    {
        Settings = options.Value;
    }

    public async Task RunAsync(IEnumerable<string> args)
    {
        Logger.LogInformation("Pdf Snipping Tool ({ProductVersion}) (https://github.com/fuse-digital/pdf-snip)", GetProductVersion());
    }
    
    private string GetProductVersion()
    {
        var assembly = Assembly.GetEntryAssembly();
        var version = FileVersionInfo.GetVersionInfo(assembly.Location);
        return version.FileVersion;
    }
}