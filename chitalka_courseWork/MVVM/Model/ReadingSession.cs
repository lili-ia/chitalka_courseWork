using CommunityToolkit.Mvvm.ComponentModel;

namespace chitalka_courseWork.MVVM.Model
{
    public partial class ReadingSession : ObservableObject
    {
        [ObservableProperty]
        private int _pagesRead;

        [ObservableProperty]
        private TimeSpan _readingTime;
        
    }
}
