namespace EpubMaker.Pages;

public class Chapter : Page
{
    public override string FileName => $"chapter-{Number}.html";
    public required uint Number { get; init; }
    public override required string Title { get; init; }
    public required string Body { get; init; }

    public override string ToString() =>
        $"""
        <?xml version='1.0' encoding='utf-8'?>
         <html xmlns="http://www.w3.org/1999/xhtml">
             <head>
               <link rel="stylesheet" type="text/css" href="{StylesheetUrl}"/>
               <title>{Title}</title>
             </head>
             <body>
             {Body}
             </body>
         </html>
        """;
}