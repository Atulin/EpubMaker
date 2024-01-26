using EpubMaker.Helpers;
using EpubMaker.Pages;

namespace EpubMaker;

public class BookNcx
{
	public required string Title { get; init; }
	public required string Author { get; init; }
	public required Page[] Pages { get; init; }

	private static (string body, uint order) CreateNavPoint(Page page)
	{
		var number = page switch
		{
			Cover => (uint)0,
			TitlePage => (uint)1,
			TableOfContents => (uint)2,
			Chapter c => c.Number + 2,
			_ => throw new ArgumentOutOfRangeException()
		};

		var chapter = number > 0 ? $"chapter{number}" : "";

		var navPoint = $"""
		                <navPoint id="{chapter}" playOrder="{number}">
		                	<navLabel><text>{page.Title}</text></navLabel>
		                	<content src="{page.FileName}" />
		                </navPoint>
		                """;
		return (navPoint, number);
	}
	
    public override string ToString()
    {
	    var navPoints = Pages
		    .Select(CreateNavPoint)
		    .OrderBy(x => x.order)
		    .Select(x => x.body);
	    
        return $"""
               <?xml version="1.0" encoding="UTF-8"?>
               <!DOCTYPE ncx PUBLIC "-//NISO//DTD ncx 2005-1//EN"
                  "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd">
               <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/" version="2005-1" xml:lang="en">
              	<head>
              		<meta name="dtb:uid" content="{Title.Friendlify()}" />
              		<meta name="dtb:depth" content="2" />
              		<meta name="dtb:totalPageCount" content="0" />
              		<meta name="dtb:maxPageNumber" content="0" />
              		<meta name="dtb:generator" content="https://github.com/Atulin/EpubMaker" />
              	</head>
              
              	<docTitle>
              		<text>{Title}</text>
              	</docTitle>
              
              	<docAuthor>
              		<text>{Author}</text>
              	</docAuthor>
              
              	<navMap>
              	{string.Join("\n\t", navPoints)}
              	</navMap>
              </ncx>
              """;
    }
}