using chitalka_courseWork.MVVM.ViewModel;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        DataContext = new HomeViewModel();
        InitializeComponent();
    }
}