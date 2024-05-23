using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
        public Statistics()
        {
            ReadingSessions = [];
            Quotes = [];
            Progress = 0;
        }
        public void UpdateProgress(int currentPageRead, int pageCount) => Progress = (100 * currentPageRead) / pageCount;
        
    }





}
