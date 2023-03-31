using System.Threading.Tasks;
using FakeItEasy;
using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents;
using FuseDigital.PdfSnip.Documents.Dto;
using Volo.Abp.DependencyInjection;
using Xunit;

namespace FuseDigital.PdfSnip.Commands;

public class SplitCommandTests : PdfSnipApplicationTestBase
{
    [Fact]
    public async Task Should_Split_Document()
    {
        // Arrange
        var service = A.Fake<IDocumentDomainService>();
        var options = new SplitOptions
        {
            PdfDocumentPath = "split-pdf-document.pdf",
            OutputDirectoryPath = ".",
        };
        var command = new SplitCommand(service)
        {
            LazyServiceProvider = GetService<IAbpLazyServiceProvider>(),
        };

        // Act
        await command.ExecuteAsync(options);

        // Assert
        A.CallTo(() => service.SplitAsync(A<SplitInput>._))
            .MustHaveHappened();
    }
}