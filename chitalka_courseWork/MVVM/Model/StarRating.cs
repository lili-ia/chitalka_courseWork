using CommunityToolkit.Mvvm.ComponentModel;

namespace chitalka_courseWork.MVVM.Model
{
    public partial class StarRating : ObservableObject
    {
        [ObservableProperty]
        private bool _isFilled;

        [ObservableProperty]
        private int _rating;

    }
}
