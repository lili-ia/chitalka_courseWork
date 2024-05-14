using chitalka_courseWork.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class LibraryView : UserControl
{
    private LibraryViewModel viewModel;
    public LibraryView(LibraryViewModel libraryVM)
    {
        viewModel= libraryVM;
        DataContext = viewModel;
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        viewModel.DeleteBookCommand.Execute(null);
    }

    private void ReadButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (viewModel.SelectedBook != null) 
        {
            viewModel.SelectedBook.ReadingStatus = ReadingStatus.InProgress;
        }
        else
        {
            MessageBox.Show("Ви не обрали книгу!");
        }
        
    }
}