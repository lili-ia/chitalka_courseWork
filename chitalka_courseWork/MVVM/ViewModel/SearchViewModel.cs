using chitalka_courseWork.MVVM.View;
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
    private RelayCommand _addBookManuallyCommand;

    [ObservableProperty]
    private ObservableCollection<Book> _searchResults;

    [ObservableProperty]
    private string? _query;

    [ObservableProperty]
    private LibraryViewModel _libraryViewModel;

    [ObservableProperty]
    private Book _manuallyAddedBook;



    public SearchViewModel(LibraryViewModel libraryViewModel)
    {
        _libraryViewModel = libraryViewModel;
        SearchCommand = new AsyncRelayCommand(Search);
        AddBookCommand = new RelayCommand(AddBook);
        AddBookManuallyCommand = new RelayCommand(GetNewBookFromUser);
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
        }
    }

    private void GetNewBookFromUser()
    {
        var inputDialog = new AddingBookManuallyDialog();

        ManuallyAddedBook = new Book("", "", "", 0, "");

        if (inputDialog.ShowDialog() == true)
        {
            ManuallyAddedBook = new Book(inputDialog.BookTitle, inputDialog.Author, "", inputDialog.PagesCount, inputDialog.CoverImagePath);
            LibraryViewModel.Books.Add(ManuallyAddedBook);
        }

    }
}