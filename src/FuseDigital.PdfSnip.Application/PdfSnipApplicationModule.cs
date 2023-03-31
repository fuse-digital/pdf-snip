using Microsoft.Extensions.DependencyInjection;
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
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PdfSnipApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PdfSnipApplicationModule>(validate: true);
        });
    }
}