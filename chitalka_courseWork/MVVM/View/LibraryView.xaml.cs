using chitalka_courseWork.MVVM.ViewModel;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class LibraryView : UserControl
{
    public LibraryView(LibraryViewModel libraryVM)
    {
        DataContext = libraryVM;
        InitializeComponent();
    }
}