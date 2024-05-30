using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace chitalka_courseWork.MVVM.Model
{
    public partial class Statistics : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ReadingSession> _readingSessions;

        [ObservableProperty]
        private ObservableCollection<string> _notes;

        [ObservableProperty]
        private int _rating; 

        [ObservableProperty]
        private int _progress; 

        [ObservableProperty]
        private int _pagesRead;

        public int TotalPageCount;

        public Statistics()
        {
            ReadingSessions = [];
            Notes = [];
            ReadingSessions.CollectionChanged += ReadingSessions_CollectionChanged;
        }

        public void SetTotalPageCount(int pageCount)
        {
            TotalPageCount = pageCount;
            UpdateProgress();
        }

        private void ReadingSessions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateProgress();
        }


        public void UpdateProgress()
        {
            PagesRead = ReadingSessions.Sum(session => session.PagesRead);

            if (TotalPageCount > 0)
                Progress = (100 * PagesRead) / TotalPageCount;
            else
                Progress = 0;
            
            OnPropertyChanged(nameof(Progress));
        }
    }
}
