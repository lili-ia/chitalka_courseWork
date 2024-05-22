using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private AsyncRelayCommand _searchCommand;

    [ObservableProperty]
    private RelayCommand _addBookCommand;

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private ObservableCollection<Book> _searchResults;

    [ObservableProperty]
    private string? _query;

    [ObservableProperty]
    private LibraryViewModel _libraryViewModel;

    public SearchViewModel(LibraryViewModel libraryViewModel)
    {
        _libraryViewModel = libraryViewModel;
        SearchCommand = new AsyncRelayCommand(Search);
        AddBookCommand = new RelayCommand(AddBook);
        SearchResults = [];
    }

    public async Task Search()
    {
        if (string.IsNullOrEmpty(Query))
        {
            MessageBox.Show("Ви нічого не ввели!");
            return;
        }
        Task<List<Book>> taskSearch = BookSearch.Search(Query);
        List<Book> booksResult = await taskSearch;

        SearchResults.Clear();

        foreach (var book in booksResult)
        {
            SearchResults.Add(book);
        }
    }

    public void AddBook()
    {
        if (SelectedBook != null)
        {
            LibraryViewModel.Books.Add(SelectedBook);
            string filePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json";
            string json = JsonConvert.SerializeObject(LibraryViewModel.Books, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}