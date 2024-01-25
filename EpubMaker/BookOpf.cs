using EpubMaker.Pages;

namespace EpubMaker;

public class BookOpf
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required Page[] Pages { get; init; }

    public override string ToString()
    {
        var items = Pages.Select(p =>
            $"""<item id="{p.FileName.Split('.')[0]}" href="{p.FileName}" media-type="application/xhtml+xml"/>""");

        var itemRefs = Pages.Select(p => $"""<itemref idref="{p.FileName.Split('.')[0]}"/>""");

        return $"""
                <?xml version="1.0"  encoding="UTF-8"?>
                <package xmlns="http://www.idpf.org/2007/opf" unique-identifier="BookId" version="2.0">
                  <metadata xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dcterms="http://purl.org/dc/terms/">
                    <dc:title>{Title}</dc:title>
                    <dc:creator opf:role="aut" opf:file-as="{Author}">{Author}</dc:creator>
                    <dc:date>{DateTime.Now}</dc:date>
                  </metadata>
                  <manifest>
                    {string.Join("\n\t\t", items)}
                    <item id="ncx" href="book.ncx" media-type="application/x-dtbncx+xml"/>
                    <item id="css_css" href="styles.css" media-type="text/css"/>
                    <item href="cover.jpg" id="cover" media-type="image/jpeg"/>
                  </manifest>
                  <spine toc="ncx">
                    {string.Join("\n\t\t", itemRefs)}
                  </spine>
                </package>
                """;
    }
}