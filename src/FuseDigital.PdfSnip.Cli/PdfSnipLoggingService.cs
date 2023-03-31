using System.IO;
using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Volo.Abp.ObjectMapping;

namespace FuseDigital.PdfSnip.Cli;

public class PdfSnipLoggingService : ILoggingService
{
    private readonly IObjectMapper _objectMapper;
    private PdfSnipOptions Settings { get; }

    public LogLevel Verbosity
    {
        get => _objectMapper.Map<LogEventLevel, LogLevel>(LoggingLevel.MinimumLevel);
        set => LoggingLevel.MinimumLevel = _objectMapper.Map<LogLevel, LogEventLevel>(value);
    }

    public string LogDirectory => Path.Combine(Settings.LocalApplicationDataPath, Settings.LogDirectory);

    private LoggingLevelSwitch LoggingLevel { get; } = new()
    {
        MinimumLevel = LogEventLevel.Information
    };

    public PdfSnipLoggingService(IOptions<PdfSnipOptions> options, IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
        Settings = options.Value;
    }

    public void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(LoggingLevel)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(LogDirectory, "pdf-snip.log"))
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}"
            )
            .CreateLogger();
    }

    public void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}