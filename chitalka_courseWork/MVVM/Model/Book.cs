using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
    private int _pagesCount;

    [ObservableProperty]
    private ReadingStatus _readingStatus;

    [ObservableProperty]
    private string? _coverImageUrl;

    [ObservableProperty]
    private Statistics _stats;

    public Book(string title, string author, string? description, int pagesCount, string? coverImageUrl)
    {
        Title = title;
        Author = author;
        Description = description;
        PagesCount = pagesCount;
        ReadingStatus = ReadingStatus.NotStarted;
        CoverImageUrl = coverImageUrl;
        Stats = new Statistics(pagesCount);
    }


    public override string ToString() => $"Title: '{Title}'\nAuthor: {Author}\nDescription: {Description}\n";
    public override int GetHashCode() => HashCode.Combine(Title, Author);
    public override bool Equals(object obj)
    {
        if (obj is Book book)
        {
            return Title == book.Title && Author == book.Author;
        }
        return false;
    } 

}
