using EpubMaker.Helpers;

namespace EpubMaker;

internal static class MetaInfContainer
{
    public static string Content => """
                                    <?xml version="1.0" encoding="utf-8"?>
                                    <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
                                      <rootfiles>
                                        <rootfile full-path="book.opf" media-type="application/oebps-package+xml"/>
                                      </rootfiles>
                                    </container>
                                    """.Minify();
}