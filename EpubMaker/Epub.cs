using System.Globalization;
using System.IO.Compression;
using EpubMaker.Pages;
using EpubMaker.Helpers;

namespace EpubMaker;

/// <summary>
/// Represents the complete <c>.epub</c> file
/// </summary>
public sealed class Epub
{
    private const string MimeType = "application/epub+zip";
    
    /// <summary>
    /// Title of the publication
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Author of the publication
    /// </summary>
    public required string Author { get; init; }
    
    /// <summary>
    /// Contents of the linked stylesheet
    /// </summary>
    public string Styles { get; init; } = "";
    
    /// <summary>
    /// List of chapters in the publication
    /// </summary>
    public required List<Chapter> Chapters { get; init; } = [];

    /// <summary>
    /// Culture of the publication, used to determine the language
    /// </summary>
    public CultureInfo Culture { get; init; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// File stream containing the cover image
    /// </summary>
    public ReadOnlyMemory<byte>? CoverImage { get; init; }

    /// <summary>
    /// Unique book identifier like ISBN ur UUID
    /// </summary>
    public string Identifier { get; init; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Generates the resulting file in memory
    /// </summary>
    /// <param name="fs"><see cref="FileStream"/> to store the generated file</param>
    public async Task Generate(FileStream fs)
    {
        Page[] pages =
        [
            new TableOfContents
            {
                Chapters = Chapters.ToArray()
            },
            new TitlePage
            {
                Title = Title
            },
            ..Chapters
        ];

        // Open file
        using var zipFile = new ZipArchive(fs, ZipArchiveMode.Create);

        // Create mimetype
        await zipFile.AddFile("mimetype", MimeType);

        // Create META-INF, directory separator here **must** be a forward slash
        await zipFile.AddFile("META-INF/container.xml", MetaInfContainer.Content);
        
        // Create NCX
        await zipFile.AddFile("book.ncx", new BookNcx
        {
            Author = Author,
            Title = Title,
            Pages = pages,
            Identifier = Identifier,
        }.ToString());

        // Create OPF
        await zipFile.AddFile("book.opf", new BookOpf
        {
            Author = Author,
            Title = Title,
            Pages = pages,
            Culture = Culture,
            HasCover = CoverImage is not null,
            Identifier = Identifier,
        }.ToString());

        // Add styles
        await zipFile.AddFile(Constants.StylesPath, Styles);
        
        // Add cover
        if (CoverImage is { } ci)
        {
            await zipFile.AddFile("cover.jpg", ci);
        }
        
        foreach (var page in pages)
        {
            await zipFile.AddFile(page.FileName, page.ToString());
        }
    }

    /// <summary>
    /// Generates the <c>.epub</c> file at a selected location
    /// </summary>
    /// <param name="outFile">Path to the desired file location</param>
    public async Task Generate(string outFile)
    {
        await using var fs = File.Open(outFile, FileMode.OpenOrCreate);
        await Generate(fs);
    }
}