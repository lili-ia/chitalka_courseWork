using chitalka_courseWork.Core;

namespace chitalka_courseWork.MVVM.ViewModel
{
    class MainViewModel : Core.ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LibraryViewCommand { get; set; }
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand StatsViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public LibraryViewModel LibraryVM { get; set; }
        public SearchViewModel SearchVM { get; set; }
        public StatsViewModel StatsVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            LibraryVM = new LibraryViewModel();
            SearchVM = new SearchViewModel();
            StatsVM = new StatsViewModel();

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            LibraryViewCommand = new RelayCommand(o =>
            {
                CurrentView = LibraryVM;
            });

            SearchViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });

            StatsViewCommand = new RelayCommand(o =>
            {
                CurrentView = StatsVM;
            });
        }
    }
}
