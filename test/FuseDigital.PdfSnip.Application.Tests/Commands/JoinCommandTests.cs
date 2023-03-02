using System.Threading.Tasks;
using FakeItEasy;
using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents;
using FuseDigital.PdfSnip.Documents.Dto;
using Volo.Abp.DependencyInjection;
using Xunit;

namespace FuseDigital.PdfSnip.Commands;

public class JoinCommandTests : PdfSnipApplicationTestBase
{
    [Fact]
    public async Task Should_Join_Documents()
    {
        // Arrange
        var service = A.Fake<IDocumentDomainService>();
        var options = new JoinOptions
        {
            OutputPdfDocumentPath = "join-pdf-documents.pdf",
            InputDirectoryPath = "."
        };
        var command = new JoinCommand(service)
        {
            LazyServiceProvider = GetService<IAbpLazyServiceProvider>(),
        };

        // Act
        await command.ExecuteAsync(options);

        // Assert
        A.CallTo(() => service.JoinAsync(A<JoinInput>._))
            .MustHaveHappened();
    }
}