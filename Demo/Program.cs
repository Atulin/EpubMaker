using EpubMaker;
using EpubMaker.Pages;

var epub = new Epub
{
    Title = "Test Epub",
    Author = "John Doe",
    Styles = "",
    BookOpf = new BookOpf(),
    MetaInfContainer = new MetaInfContainer(),
    Pages = [
        new Chapter
        {
            Number = 1,
            Title = "Lorem Ipsum",
            Body = "Hello World!"
        },
        new Chapter
        {
            Number = 2,
            Title = "Dolor Site",
            Body = "New stuff"
        },
        new Chapter
        {
            Number = 3,
            Title = "Amet",
            Body = "Goodbye World!"
        },
    ]
};

await epub.Generate("book.epub");
