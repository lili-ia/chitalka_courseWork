using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.ViewModel
{
    class MainViewModel : Core.ObservableObject
    {
        public LibraryViewModel LibraryVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            LibraryVM = new LibraryViewModel();
            CurrentView = LibraryVM;
        }
    }
}
