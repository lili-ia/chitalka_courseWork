using chitalka_courseWork.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class MainView : UserControl
{
    public MainView()
    {
        DataContext = new MainViewModel();
        InitializeComponent();
        Loaded += MainView_Loaded;
    }

    private void MainView_Loaded(object sender, RoutedEventArgs e) => UpdateGreeting();

    private void UpdateGreeting()
    {
        Loaded -= MainView_Loaded;
        int hour = DateTime.Now.Hour;

        if (hour >= 4 && hour < 12)
            greeting.Text = "Доброго ранку, User!";
        else if (hour >= 12 && hour < 18)
            greeting.Text = "Добрий день, User!";
        else
            greeting.Text = "Добрий вечір, User!";
    }
}