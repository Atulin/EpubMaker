namespace EpubMaker.Templates;

public record BookNcx(
    string Uid,
    string Title,
    string Author,
    IEnumerable<BookNcxNavPoint> NavPoints
);