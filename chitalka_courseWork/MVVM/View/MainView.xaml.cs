using chitalka_courseWork.MVVM.ViewModel;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class MainView : UserControl
{
    public MainView()
    {
        DataContext = new MainViewModel();
        InitializeComponent();
    }
}