using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;


namespace chitalka_courseWork.MVVM.ViewModel;

public partial class LibraryViewModel : ObservableObject
{
    [ObservableProperty]
    private RelayCommand _deleteBookCommand;

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private ObservableCollection<Book> _books;

    [ObservableProperty]
    private ObservableCollection<Book> _inProgressBooks;

    [ObservableProperty]
    private ObservableCollection<Book> _readBooks;

    private readonly string _filePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json";

    public LibraryViewModel()
    {
        string jsonDataAll = File.ReadAllText("C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json");
        Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonDataAll);
        FilterBooks();
        Books.CollectionChanged += Books_CollectionChanged;

        DeleteBookCommand = new RelayCommand(DeleteBook);
    }

    private void Books_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateBooksCollection();
        FilterBooks();
    }

    public void UpdateBooksCollection()
    {
        string json = JsonConvert.SerializeObject(Books, Formatting.Indented);
        File.WriteAllText(_filePath, json);
        Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
        FilterBooks();
    }

    private void FilterBooks()
    {
        InProgressBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.InProgress));
        ReadBooks = new ObservableCollection<Book>(Books.Where(book => book.ReadingStatus == ReadingStatus.Read));
    }

    public void DeleteBook()
    {
        if (SelectedBook != null)
        {
            Books.Remove(SelectedBook);
            SelectedBook = null;
            UpdateBooksCollection();
        }
        else
        {
            MessageBox.Show("Ви не обрали книгу!");
        }
    }

}
