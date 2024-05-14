namespace chitalka_courseWork;

public class Book
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Author { get; set; }
    public int? PagesCount { get; set; }

    public Book(string title, string author, string? description, int? pagesCount)
    {
        Title = title;
        Author = author;
        Description = description;
        PagesCount = pagesCount;
    }


    public override string ToString() => $"Title: '{Title}'\nAuthor: {Author}\nDescription: {Description}\n";
}
