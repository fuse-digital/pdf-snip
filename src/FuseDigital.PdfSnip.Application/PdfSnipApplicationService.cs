using System.Diagnostics;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using FuseDigital.PdfSnip.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace FuseDigital.PdfSnip;

public class PdfSnipApplicationService : ApplicationService
{
    private PdfSnipOptions Settings { get; }

    public PdfSnipApplicationService(IOptions<PdfSnipOptions> options)
    {
        Settings = options.Value;
    }

    public async Task RunAsync(IEnumerable<string> args)
    {
        Logger.LogInformation("Pdf Snipping Tool ({ProductVersion}) (https://github.com/fuse-digital/pdf-snip)", GetProductVersion());
        try
        {
            var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments(args, GetOptions());
            await parserResult.WithParsedAsync<IPdfCommandOptions>(RunCommandAsync);
            await parserResult.WithNotParsedAsync(errors => DisplayHelpAsync(parserResult, errors));
        }
        catch (Exception exception)
            when (exception is AbpValidationException or EntityNotFoundException or BusinessException)
        {
            Logger.LogBusinessException(exception);
        }
        catch (Exception exception)
        {
            Logger.LogException(exception);
            Logger.LogWithLevel(exception.GetLogLevel(), $"Please see the logs for more information.");
        }
    }
    
    private string GetProductVersion()
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        return version?.ToString();
    }
    
    private Task DisplayHelpAsync<T>(ParserResult<T> result, IEnumerable<Error> errors)
    {
        var errorList = errors.ToList();
        if (errorList.IsHelp())
        {
            Console.WriteLine(HelpText.AutoBuild(result));
        }
        else if (errorList.IsVersion())
        {
            Console.WriteLine(GetProductVersion());
        }
        else
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = true;
                h.Heading = "QuickSetup (QUP)";
                h.Copyright = $"Copyright (c) {DateTime.Now.Year} by Fuse Digital (PTY) Limited";
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }
        return Task.CompletedTask;
    }

    private async Task RunCommandAsync(IPdfCommandOptions options)
    {
        var command = (IPdfCommandAsync) LazyServiceProvider.LazyGetRequiredService(options.GetCommandType());
        await command.ExecuteAsync(options);
    }

    private static Type[] GetOptions()
    {
        var options = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPdfCommandOptions)))
                && t.GetCustomAttribute<VerbAttribute>() != null)
            .OrderBy(x => x.Name)
            .ToArray();

        return options;
    }
}