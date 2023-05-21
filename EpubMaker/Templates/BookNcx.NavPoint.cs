namespace EpubMaker.Templates;

public record BookNcxNavPoint(
    string Id,
    uint Order,
    string Label,
    string Src
);