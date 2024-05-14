namespace chitalka_courseWork;
public enum ReadingStatus
{
    NotStarted, 
    InProgress, 
    Read
}

public class Book
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Author { get; set; }
    public int? PagesCount { get; set; }
    public ReadingStatus ReadingStatus { get; set; }
    public Book(string title, string author, string? description, int? pagesCount)
    {
        Title = title;
        Author = author;
        Description = description;
        PagesCount = pagesCount;
        ReadingStatus = ReadingStatus.NotStarted;
    }


    public override string ToString() => $"Title: '{Title}'\nAuthor: {Author}\nDescription: {Description}\n";


}
