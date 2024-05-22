using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    public Dictionary<Book, Statistics> BookStats { get; set; }

}
