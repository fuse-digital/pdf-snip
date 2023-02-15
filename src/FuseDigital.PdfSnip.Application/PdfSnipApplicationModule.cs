using Volo.Abp.Application;
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