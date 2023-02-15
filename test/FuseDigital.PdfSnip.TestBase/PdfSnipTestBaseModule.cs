using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(PdfSnipDomainModule)
)]
public class PdfSnipTestBaseModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);
        context.ServiceProvider.GetRequiredService<ILoggingService>().CreateLogger();
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        base.OnApplicationShutdown(context);
        context.ServiceProvider.GetRequiredService<ILoggingService>().CloseAndFlush();
    }
}