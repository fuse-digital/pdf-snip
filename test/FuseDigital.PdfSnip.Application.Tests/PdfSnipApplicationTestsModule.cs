using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip;

[DependsOn(
    typeof(PdfSnipApplicationModule),
    typeof(PdfSnipDomainTestsModule)
)]
public class PdfSnipApplicationTestsModule : AbpModule
{
}