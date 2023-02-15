using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace FuseDigital.PdfSnip;

[DependsOn(
    typeof(AbpDddDomainModule)
)]
public class PdfSnipDomainModule : AbpModule
{
}