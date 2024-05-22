using CommunityToolkit.Mvvm.ComponentModel;

namespace chitalka_courseWork;
public enum ReadingStatus
{
    NotStarted, 
    InProgress, 
    Read
}

public partial class Book : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private string _author;

    [ObservableProperty]
    private int? _pagesCount;

    [ObservableProperty]
    private ReadingStatus _readingStatus;

    [ObservableProperty]
    private string? _coverImageUrl;
    public Book(string title, string author, string? description, int? pagesCount, string? coverImageUrl)
    {
        Title = title;
        Author = author;
        Description = description;
        PagesCount = pagesCount;
        ReadingStatus = ReadingStatus.NotStarted;
        CoverImageUrl = coverImageUrl;
    }


    public override string ToString() => $"Title: '{Title}'\nAuthor: {Author}\nDescription: {Description}\n";


}
