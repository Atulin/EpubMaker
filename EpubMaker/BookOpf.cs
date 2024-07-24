using System.Globalization;
using EpubMaker.Pages;

namespace EpubMaker;

internal class BookOpf
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required Page[] Pages { get; init; }
    public required CultureInfo Culture { get; init; }
    public bool HasCover { get; init; }
    public required string Identifier { get; init; }

    public override string ToString()
    {
        var items = Pages.Select(p =>
            $"""<item id="{p.FileName.Split('.')[0]}" href="{p.FileName}" media-type="application/xhtml+xml"/>""");

        var itemRefs = Pages.Select(p => $"""<itemref idref="{p.FileName.Split('.')[0]}"/>""");

        return $"""
                <?xml version="1.0"  encoding="UTF-8"?>
                <package xmlns="http://www.idpf.org/2007/opf" unique-identifier="book-id" version="2.0">
                  <metadata xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dcterms="http://purl.org/dc/terms/">
                    <dc:title>{Title}</dc:title>
                    <dc:creator opf:role="aut" opf:file-as="{Author}">{Author}</dc:creator>
                    <dc:date>{DateTime.Now:s}</dc:date>
                    <dc:identifier id="book-id">{Identifier}</dc:identifier>
                    <dc:language>{Culture.TwoLetterISOLanguageName}</dc:language>
                  </metadata>
                  <manifest>
                    {string.Join("\n\t\t", items)}
                    <item id="ncx" href="book.ncx" media-type="application/x-dtbncx+xml"/>
                    <item id="css_css" href="{Constants.StylesPath}" media-type="text/css"/>
                    {(HasCover ? """<item href="cover.jpg" id="cover" media-type="image/jpeg"/>""" : "")}
                  </manifest>
                  <spine toc="ncx">
                    {string.Join("\n\t\t", itemRefs)}
                  </spine>
                </package>
                """;
    }
}