using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace FuseDigital.PdfSnip;

public abstract class PdfSnipTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
    where TStartupModule : IAbpModule
{
}