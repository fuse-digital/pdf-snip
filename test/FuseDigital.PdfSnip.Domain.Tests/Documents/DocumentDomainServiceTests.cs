using System;
using System.IO;
using System.Threading.Tasks;
using FuseDigital.PdfSnip.Documents.Dto;
using Shouldly;
using Xunit;

namespace FuseDigital.PdfSnip.Documents;

public class DocumentDomainServiceTests : PdfSnipDomainTestBase
{
    [Fact]
    public async Task Should_Split_All_Pages_Into_Seperate_Documents()
    {
        // Arrange
        var document = new DocumentDomainService();
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
        var input = new SplitInput
        {
            OutputDirectoryPath = outputPath,
            PdfDocumentPath = Path.Combine(outputPath, "split-document.pdf"),
        };
        await CopyFileAsync("split-document.pdf", input.PdfDocumentPath);

        // Act
        var output = await document.SplitAsync(input);
        
        // Assert
        output.ShouldNotBeEmpty();
        output.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Should_Throw_Exception_If_Document_Not_Found()
    {
        // Arrange
        var document = new DocumentDomainService();
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);
        
        var input = new SplitInput
        {
            OutputDirectoryPath = outputPath,
            PdfDocumentPath = Path.Combine(outputPath, "split-document.pdf"),
        };

        // Act
        var exception = await Should.ThrowAsync<FileNotFoundException>(async () =>
        {
            await document.SplitAsync(input);
        });
        
        // Assert
        exception.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Should_Throw_Exception_If_Output_Directory_Not_Found()
    {
        // Arrange
        var document = new DocumentDomainService();
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
        var input = new SplitInput
        {
            OutputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString()),
            PdfDocumentPath = Path.Combine(outputPath, "split-document.pdf"),
        };
        await CopyFileAsync("split-document.pdf", input.PdfDocumentPath);

        // Act
        var exception = await Should.ThrowAsync<DirectoryNotFoundException>(async () =>
        {
            await document.SplitAsync(input);
        });
        
        // Assert
        exception.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Join_All_Documents_Into_Single_Document()
    {
        var document = new DocumentDomainService();
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
        var splitInput = new SplitInput
        {
            OutputDirectoryPath = outputPath,
            PdfDocumentPath = Path.Combine(outputPath, "split-document.pdf"),
        };
        await CopyFileAsync("split-document.pdf", splitInput.PdfDocumentPath);
        await document.SplitAsync(splitInput);

        var joinInput = new JoinInput
        {
            InputDirectoryPath = outputPath,
            OutputPdfDocumentPath = Path.Combine(outputPath, "join-documents.pdf")
        };
        
        // Act
        var output = await document.JoinAsync(joinInput);
        
        // Assert
        output.ShouldNotBeEmpty();
        output.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task Should_Throw_Exception_If_Directory_Not_Found()
    {
        // Arrange
        var document = new DocumentDomainService();
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());

        var input = new JoinInput
        {
            InputDirectoryPath = outputPath,
            OutputPdfDocumentPath = Path.Combine(outputPath, "join-documents.pdf")
        };

        // Act
        var exception = await Should.ThrowAsync<DirectoryNotFoundException>(async () =>
        {
            await document.JoinAsync(input);
        });
        
        // Assert
        exception.ShouldNotBeNull();
    }
}