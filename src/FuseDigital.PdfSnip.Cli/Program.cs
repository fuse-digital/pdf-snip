using System;
using System.Threading.Tasks;
using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Volo.Abp;

namespace FuseDigital.PdfSnip.Cli;

public static class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        using var application = await AbpApplicationFactory.CreateAsync<PdfSnipCliModule>(options =>
        {
            options.UseAutofac();
            options.Services.AddLogging(c => c.AddSerilog());
        });

        await application.InitializeAsync();

        var loggingService = application.ServiceProvider.GetRequiredService<ILoggingService>();
        loggingService.CreateLogger();

        await application.ServiceProvider
            .GetRequiredService<PdfSnipApplicationService>()
            .RunAsync(args);
        
        await application.ShutdownAsync();

        loggingService.CloseAndFlush();
    }
}

