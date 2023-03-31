using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip;

[DependsOn(
    typeof(PdfSnipTestBaseModule)
)]
public class PdfSnipDomainTestsModule : AbpModule
{
}