using chitalka_courseWork.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private RelayCommand _homeRelayCommand;
    [ObservableProperty]
    private RelayCommand _libraryRelayCommand;
    [ObservableProperty]
    private RelayCommand _searchRelayCommand;
    [ObservableProperty]
    private RelayCommand _statsRelayCommand;

    [ObservableProperty]
    private HomeView _homeView;
    [ObservableProperty]
    private LibraryView _libraryView;
    [ObservableProperty]
    private SearchView _searchView;
    [ObservableProperty]
    private StatsView _statsView;

    [ObservableProperty]
    private object _currentView;

    public MainViewModel()
    {
        HomeView = new HomeView();
        LibraryView = new LibraryView();
        SearchView = new SearchView();
        StatsView = new StatsView();

        CurrentView = HomeView;

        HomeRelayCommand = new RelayCommand(() => CurrentView = HomeView);
        LibraryRelayCommand = new RelayCommand(() => CurrentView = LibraryView);
        SearchRelayCommand = new RelayCommand(() => CurrentView = SearchView);
        StatsRelayCommand = new RelayCommand(() => CurrentView = StatsView);


    }

}