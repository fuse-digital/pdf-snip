using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace FuseDigital.PdfSnip;

public abstract class PdfCommandAsync : IPdfCommandAsync
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    private ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    private ILoggingService LoggingService => LazyServiceProvider.LazyGetRequiredService<ILoggingService>();

    protected ILogger Logger => LazyServiceProvider
        .LazyGetService<ILogger>(provider => LoggerFactory?
            .CreateLogger(GetType().FullName) ?? NullLogger.Instance);

    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetRequiredService<IObjectMapper>();

    public virtual Task ExecuteAsync(IPdfCommandOptions options)
    {
        if (options is PdfCommandOptions commandOptions)
        {
            LoggingService.Verbosity = commandOptions.Verbosity;
        }

        return Task.CompletedTask;
    }
}