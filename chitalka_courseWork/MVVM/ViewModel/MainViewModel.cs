using chitalka_courseWork.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private RelayCommand _libraryRelayCommand;
    [ObservableProperty]
    private RelayCommand _searchRelayCommand;

    [ObservableProperty]
    private LibraryView _libraryView;
    [ObservableProperty]
    private SearchView _searchView;

    [ObservableProperty]
    private object _currentView;

    public MainViewModel()
    {
        LibraryViewModel libraryViewModel = new LibraryViewModel();
        LibraryView = new LibraryView(libraryViewModel);
        SearchView = new SearchView(libraryViewModel);

        CurrentView = LibraryView;

        LibraryRelayCommand = new RelayCommand(() => CurrentView = LibraryView);
        SearchRelayCommand = new RelayCommand(() => CurrentView = SearchView);
    }

}