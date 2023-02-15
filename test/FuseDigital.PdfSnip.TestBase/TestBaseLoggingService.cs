using System.IO;
using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FuseDigital.PdfSnip;

public class TestBaseLoggingService : ILoggingService
{
    public LogLevel Verbosity { get; set; }

    public string LogDirectory => "../../../../../test-logs/";

    public void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(LogDirectory, "pdf-snip-unit-test-logs.txt"))
            .CreateLogger();
    }

    public void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}