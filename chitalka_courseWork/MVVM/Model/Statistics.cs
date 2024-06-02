using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
        }

        public void SetTotalPageCount(int pageCount)
        {
            TotalPageCount = pageCount;
            UpdateProgress();
        }

        partial void OnReadingSessionsChanged(ObservableCollection<ReadingSession> value)
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
