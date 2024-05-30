using System.Windows;

namespace chitalka_courseWork.MVVM.View
{

    public partial class AddingBookManuallyDialog : Window
    {
        public string Title {  get; set; }
        public string Author { get; set; }
        public int PagesCount { get; set; }
        public AddingBookManuallyDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Title = TitleTextBox.Text;
            Author = AuthorTextBox.Text;

            if (int.TryParse(PagesAmountTextBox.Text, out int pages) && pages >= 0)
            {
                PagesCount = pages;
                DialogResult = true;
            }
        }
    }
}
