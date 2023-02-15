using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FuseDigital.PdfSnip;

public abstract class PdfSnipDomainTestBase : PdfSnipTestBase<PdfSnipDomainTestsModule>
{
    private byte[] GetFileContents(string fileName)
    {
        var fullyQualifiedName = $"{GetType().Namespace}.{fileName}";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullyQualifiedName);
        return stream?.GetAllBytes();
    }

    protected async Task CopyFileAsync(string source, string destination)
    {
        var content = GetFileContents(source);
        if (content != null)
        {
            var directoryFullName = new FileInfo(destination).Directory?.FullName;
            if (directoryFullName != null)
            {
                Directory.CreateDirectory(directoryFullName);
            }

            await File.WriteAllBytesAsync(destination, content);
        }
    }
}