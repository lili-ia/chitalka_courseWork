using System.Windows;

namespace chitalka_courseWork.MVVM.View
{
    public partial class PagesReadDialog : Window
    {
        public int PagesRead { get; private set; }

        public PagesReadDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PagesReadTextBox.Text, out int pages))
            {
                PagesRead = pages;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
