using System.IO.Compression;
using EpubMaker.Pages;
using EpubMaker.Helpers;

namespace EpubMaker;

public class Epub
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public string MimeType { get; init; } = "application/epub+zip";
    public string Styles { get; init; } = "";
    public required List<Page> Pages { get; init; } = [];

    public async Task Generate(string outFile)
    {
        Pages.Add(new TableOfContents
        {
            Chapters = Pages.OfType<Chapter>().ToArray()
        });
        
        // Open file
        await using var fs = File.Open(outFile, FileMode.OpenOrCreate);
        using var zipFile = new ZipArchive(fs, ZipArchiveMode.Create);
        
        // Create mimetype
        await zipFile.AddFile("mimetype", MimeType);
        
        // Create META-INF
        await zipFile.AddFile("META-INF/container.xml", MetaInfContainer.Content);

        // Create NCX
        await zipFile.AddFile("book.ncx", new BookNcx
        {
            Author = Author,
            Title = Title,
            Pages = Pages.ToArray()
        }.ToString());
        
        // Create OPF
        await zipFile.AddFile("book.opf", new BookOpf
        {
            Author = Author,
            Title = Title,
            Pages = Pages.ToArray()
        }.ToString());
        
        // Add styles
        await zipFile.AddFile("styles.css", Styles);
        
        
        var pageTasks = Pages.Select(p => zipFile.AddFile(p.FileName, p.ToString()));
        await Task.WhenAll(pageTasks);
    }

}