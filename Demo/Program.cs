using System.Globalization;
using EpubMaker;
using EpubMaker.Pages;

const string outFile = "./book.epub";

File.Delete(outFile);

var epub = new Epub
{
    Title = "Test Epub",
    Author = "John Doe",
    Styles = """
             p {
                 font-family: "Palatino", "Times New Roman", Caecilia, serif;
             }
             """,
    Culture = CultureInfo.CreateSpecificCulture("en-US"),
    Chapters = [
        new Chapter
        {
            Number = 1,
            Title = "Lorem Ipsum",
            Body = "<p>Hello World!</p>"
        },
        new Chapter
        {
            Number = 2,
            Title = "Dolor Site",
            Body = "<p>New stuff</p>"
        },
        new Chapter
        {
            Number = 3,
            Title = "Amet",
            Body = "<p>Goodbye World!</p>"
        },
    ]
};

await epub.Generate(outFile);
