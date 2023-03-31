namespace FuseDigital.PdfSnip;

public class PdfSnipOptions
{
    public string UserProfile { get; set; } = "~";

    public string LocalApplicationDataPath { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PdfSnip");

    public string LogDirectory { get; set; } = "logs";
}