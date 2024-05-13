using chitalka_courseWork.MVVM.ViewModel;
using System.Windows.Controls;

namespace chitalka_courseWork.MVVM.View;

public partial class SearchView : UserControl
{
    private SearchViewModel viewModel;
    public SearchView()
    {
        viewModel = new SearchViewModel();
        DataContext = viewModel;
        InitializeComponent();
    }

    private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        viewModel.Query = args.QueryText;
        viewModel.SearchCommand.Execute(null);
    }
}