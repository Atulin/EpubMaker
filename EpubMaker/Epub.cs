using EpubMaker.Pages;

namespace EpubMaker;

public class Epub
{
    public required string Title { get; set; }
    public string MimeType { get; set; } = "application/epub+zip";
    public required string Styles { get; set; }
    public required MetaInfContainer MetaInfContainer { get; set; }
    public required BookNcx BookNcx { get; set; }
    public required BookOpf BookOpf { get; set; }
    public required List<Page> Pages { get; set; } = new();

}