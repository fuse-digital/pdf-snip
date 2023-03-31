# Pdf Snipping Tool (PdfSnip)
Pdf Snip is a powerful and easy-to-use command line tool designed to help you manage your PDF documents. With Pdf Snip, you can quickly encrypt, decrypt, split, and join PDF documents. This open-source project is hosted on GitHub and the package is available on NuGet. It is cross-platform and can be installed via the dotnet tool chain.

## Features

- Split PDF documents into individual pages
- Join multiple PDF documents into a single file
- Encrypt PDF documents to protect sensitive information
- Decrypt encrypted PDF documents

## Installation

Pdf Snip can be installed on Windows, macOS, and Linux using the .NET tool chain. Before installing Pdf Snip, make sure you have the .NET SDK installed. If you don't have the .NET SDK installed, you can download it from [here](https://dotnet.microsoft.com/download).

### Windows, macOS, and Linux

Open your terminal or command prompt and run the following command:

```csharp
dotnet tool install --global FuseDigital.PdfSnip
```

This command will install Pdf Snip as a global tool on your system.


## Usage

### Split out a pdf document

To split a PDF document into individual pages, use the split command followed by the source PDF file and the output directory:

```bash
pdfsnip split {source_pdf} {output_directory}
```

Example:

```bash
pdfsnip split example.pdf .
```

This will split example.pdf into individual pages and save them in the current directory.

### Join multiple pdf documents

To join a multiple PDF documents into a single document, use the join command followed by the source directory and the output pdf document:

```bash
pdfsnip join {source_directory} {output_pdf_document}
```

Example:

```bash
pdfsnip join . example.pdf
```

This will join all the *.pdf documents in the current directory into the example.pdf document.