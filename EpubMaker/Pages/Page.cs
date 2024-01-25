using EpubMaker.Helpers;

namespace EpubMaker.Pages;

public abstract class Page
{
    public virtual string FileName => $"{Title.Friendlify()}.html";
    public abstract string Title { get; init; }
    public string StylesheetUrl { get; init; }
}