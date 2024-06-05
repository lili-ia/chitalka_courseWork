using chitalka_courseWork.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class LibraryView : UserControl
{
    private readonly LibraryViewModel viewModel;

    public LibraryView(LibraryViewModel libraryVM)
    {
        viewModel = libraryVM;
        DataContext = viewModel;
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.DeleteBookCommand.Execute(null);
    }

    private void ReadButton_Click(object sender, RoutedEventArgs e)
    {

        if (viewModel.SelectedBook != null) 
        {

            viewModel.SelectedBook.ReadingStatus = ReadingStatus.InProgress;
            viewModel.UpdateBooksCollection();
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

    private void ClearFiltersSortingButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.ClearFiltersSortingCommand.Execute(null);
    }

    private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        viewModel.Query = args.QueryText;
    }

    private void AddNoteButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.NoteContent = NoteTextBox.Text;
        viewModel.AddNoteCommand.Execute(null);
        NoteTextBox.Text = null;
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.EditModeCommand.Execute(null);
    }

    
}