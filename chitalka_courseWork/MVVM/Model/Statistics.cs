using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace chitalka_courseWork.MVVM.Model
{
    public partial class Statistics : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ReadingSession> _readingSessions;

        [ObservableProperty]
        private ObservableCollection<string> _quotes;

        [ObservableProperty]
        private int _rating; // rating is 1 to 5 stars

        [ObservableProperty]
        private int _progress; // progress is 0 to 100 percent

        [ObservableProperty]
        private int _pagesRead;

        private int _totalPageCount;
        public Statistics()
        {
            ReadingSessions = new ObservableCollection<ReadingSession>();
            Quotes = new ObservableCollection<string>();
        }

        public Statistics(int pageCount) : this()
        {
            ReadingSessions.CollectionChanged += ReadingSessions_CollectionChanged;
            _totalPageCount = pageCount;
            PagesRead = ReadingSessions.Sum(session => session.PagesRead);
            Progress = 100 * PagesRead / _totalPageCount;
        }

        private void ReadingSessions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show($"ReadingSessions.Count = {ReadingSessions.Count}");
            UpdateProgress();
        }

        partial void OnReadingSessionsChanged(ObservableCollection<ReadingSession> value)
        {
            MessageBox.Show($"PagesRead = {PagesRead}");
            UpdateProgress();
        }

        public void UpdateProgress()
        {
            PagesRead = ReadingSessions.Sum(session => session.PagesRead);
            if (_totalPageCount > 0) 
                Progress = (100 * PagesRead) / _totalPageCount;
        }

    }

}
