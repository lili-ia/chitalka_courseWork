using chitalka_courseWork.MVVM.Model;
using chitalka_courseWork.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class LibraryView : UserControl
{
    private LibraryViewModel viewModel;
    public LibraryView(LibraryViewModel libraryVM)
    {
        viewModel = libraryVM;
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
            viewModel.UpdateBooksCollection();
            viewModel.FilterBooks();
        }
        else
        {
            MessageBox.Show("Ви не обрали книгу!");
        }
        
    }

    private void StartTimerButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.StartReadingSessionCommand.Execute(null);
    }

    private void StopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.StopReadingSessionCommand.Execute(null);
    }

    private void SortButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Sorting!");
    }
}