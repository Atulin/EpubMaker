using System.Diagnostics;
using System.IO.Compression;
using System.Text.Encodings.Web;
using EpubMaker.Pages;
using EpubMaker.Helpers;

namespace EpubMaker;

public class Epub
{
    public required string Title { get; set; }
    public required string Author { get; init; }
    public string MimeType { get; set; } = "application/epub+zip";
    public required string Styles { get; set; }
    public required MetaInfContainer MetaInfContainer { get; set; }
    public BookNcx? BookNcx { get; set; }
    public required BookOpf BookOpf { get; set; }
    public required List<Page> Pages { get; set; } = new();

    public async Task Generate(string outFile)
    {
        var tmp = Path.Join(Path.GetTempPath(), "epub-maker", Title.Friendlify());

        if (!Directory.Exists(tmp))
        {
            Directory.CreateDirectory(tmp);
        }
        
        Pages.Add(new TableOfContents
        {
            Chapters = Pages.OfType<Chapter>().ToArray()
        });

        BookNcx = new BookNcx
        {
            Author = Author,
            Title = Title,
            Chapters = Pages.ToArray()
        };
        await File.WriteAllTextAsync(Path.Join(tmp, "book.ncx"), BookNcx.ToString());

        Process.Start("explorer.exe", tmp);
        
        Console.WriteLine(tmp);
        var pageTasks = Pages.Select(p => File.WriteAllTextAsync(Path.Join(tmp, p.FileName), p.ToString()));
        await Task.WhenAll(pageTasks);
        
        ZipFile.CreateFromDirectory(tmp, outFile);
    }

}