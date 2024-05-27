using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace chitalka_courseWork.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<StarRating> _starRatings;

        [ObservableProperty]
        private RelayCommand<int> _rateCommand;

        [ObservableProperty]
        private int _selectedRating;
        public HomeViewModel()
        {
            StarRatings = new ObservableCollection<StarRating>
            {
                new StarRating { Rating = 1 },
                new StarRating { Rating = 2 },
                new StarRating { Rating = 3 },
                new StarRating { Rating = 4 },
                new StarRating { Rating = 5 }
            };
            RateCommand = new RelayCommand<int>(Rate);
        }
        public void Rate(int rating)
        {
            SelectedRating = rating;
            foreach (var star in StarRatings)
            {
                star.IsFilled = star.Rating <= rating;
            }
        }
    }
}
