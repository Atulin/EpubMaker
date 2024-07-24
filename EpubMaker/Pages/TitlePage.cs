namespace EpubMaker.Pages;

internal class TitlePage : Page
{
    public required override string Title { get; init; }
    public override string FileName => "title.html";

    private const string CssOverride = // lang=css
        """
        h1 {
            width: 100%;
            text-align: center;
            font-family: sans-serif SansSerif;
        }                                
        """;

    public override string ToString()
    {
        return $"""
                 <?xml version='1.0' encoding='utf-8'?>
                 <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
                     <head>
                         <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
                         <meta name="calibre:cover" content="true"/>
                         <link rel="stylesheet" type="text/css" href="{Constants.StylesPath}"/>
                         <style type="text/css">{CssOverride}</style>
                         <title>{Title}</title>
                     </head>
                     <body>
                        <h1>{Title}</h1>
                     </body>
                 </html>
                 
                 """;
    }
}