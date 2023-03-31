namespace FuseDigital.PdfSnip;

public interface IPdfCommandAsync
{
    Task ExecuteAsync(IPdfCommandOptions options);
}