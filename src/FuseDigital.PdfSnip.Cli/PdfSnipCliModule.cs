using System;
using System.IO;
using System.Reflection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip.Cli;

[DependsOn(
    typeof(PdfSnipApplicationModule),
    typeof(AbpAutofacModule)
)]
public class PdfSnipCliModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        Configure<PdfSnipOptions>(options =>
        {
#if DEBUG
            var debugPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            options.UserProfile = Path.Combine(debugPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(options.UserProfile);
#else
            options.UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif
        });
    }
}