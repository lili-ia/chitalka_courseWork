using chitalka_courseWork.MVVM.ViewModel;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class SearchView : UserControl
{
    private SearchViewModel viewModel;
    public SearchView(LibraryViewModel libraryViewModel)
    {
        viewModel = new SearchViewModel(libraryViewModel);
        DataContext = viewModel;
        InitializeComponent();
    }

    private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        viewModel.Query = args.QueryText;
        viewModel.SearchCommand.Execute(null);
    }

    private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        viewModel.AddBookCommand.Execute(null);
    }
}