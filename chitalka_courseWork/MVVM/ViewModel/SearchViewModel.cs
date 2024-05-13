using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private AsyncRelayCommand _searchCommand;
    [ObservableProperty]
    private Book? _selectedBook;
    [ObservableProperty]
    private List<Book> _searchResults;
    [ObservableProperty]
    private string? _query;

    public SearchViewModel()
    {
        SearchCommand = new AsyncRelayCommand(Search);
        SearchResults = [];
    }

    public async Task Search()
    {
        if (string.IsNullOrEmpty(Query))
        {
            return;
        }
        Task<List<Book>> taskSearch = BookSearch.Search(Query);
        List<Book> booksResult = await taskSearch;
        SearchResults.Clear();

        MessageBox.Show(Query);
        

        //for (int i = 0; i < 10; i++) 
        //{
        //    searchResults.Add(new Book($"title{i}", $"author{i}", $"desc{i}", i));
        //}

            //foreach (Book book in results)
            //{
            //    SearchResults.Add(book); 
            //}
    }
}