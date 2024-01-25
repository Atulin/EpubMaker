namespace EpubMaker.Templates;

public record BookOpf
{
    public BookOpf(
        string title,
        string language,
        string identifier,
        string description,
        string publisher,
        string relation,
        string creator,
        DateTime date,
        string source,
        IEnumerable<BookOpfManifestItem> manifestItems
    )
    {
        Title = title;
        Language = language;
        Identifier = identifier;
        Description = description;
        Publisher = publisher;
        Relation = relation;
        Creator = creator;
        Date = date;
        Source = source;
        ManifestItems = new List<BookOpfManifestItem>
        {
            new("coverPage", "CoverPage.html", "application/xhtml+xml"),
            new("coverImage", "images/cover.jpg", "image/jpeg"),
            new("ncx", "book.ncx", "application/x-dtbncx+xml"),
            new("css", "style.css", "text/css")
        };
        ManifestItems.ToList().AddRange(manifestItems);
    }

    public string Title { get; init; }
    public string Language { get; init; }
    public string Identifier { get; init; }
    public string Description { get; init; }
    public string Publisher { get; init; }
    public string Relation { get; init; }
    public string Creator { get; init; }
    public DateTime Date { get; init; }
    public string Source { get; init; }
    public IEnumerable<BookOpfManifestItem> ManifestItems { get; init; }

    public void Deconstruct(
        out string title,
        out string language,
        out string identifier,
        out string description,
        out string publisher,
        out string relation,
        out string creator,
        out DateTime date,
        out string source,
        out IEnumerable<BookOpfManifestItem> manifestItems)
    {
        title = Title;
        language = Language;
        identifier = Identifier;
        description = Description;
        publisher = Publisher;
        relation = Relation;
        creator = Creator;
        date = Date;
        source = Source;
        manifestItems = ManifestItems;
    }
}