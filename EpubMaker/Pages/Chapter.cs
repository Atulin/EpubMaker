namespace EpubMaker.Pages;

/// <summary>
/// Represents a chapter, or otherwise a unit of the publication
/// </summary>
public sealed class Chapter : Page
{
    /// <summary>
    /// Allows to override the file name of the chapter.
    /// By default, it's <c>chapter-</c><see cref="Chapter.Number"/><c>.html</c>
    /// </summary>
    public override string FileName => $"chapter-{Number}.html";
    
    /// <summary>
    /// Represents the number of the chapter, value they will be ordered by
    /// </summary>
    public required uint Number { get; init; }
    
    /// <summary>
    /// Title of the chapter
    /// </summary>
    public override required string Title { get; init; }
    
    /// <summary>
    /// Chapter's body, formatted as valid HTML
    /// </summary>
    public required string Body { get; init; }
    
    /// <summary>
    /// Allows to override the global stylesheet.
    /// Contents of this property will be placed in a <c>style</c> element after the global stylesheet.
    /// </summary>
    public string? CssOverride { get; init;  }

    public override string ToString() =>
        $"""
        <?xml version='1.0' encoding='utf-8'?>
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <link rel="stylesheet" type="text/css" href="{Constants.StylesPath}"/>
                {(CssOverride is null ? "" : $"<style type=\"text/css\">{CssOverride}</style>")}
                <title>{Title}</title>
            </head>
            <body>
            {Body}
            </body>
        </html>
        """;
}