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
}