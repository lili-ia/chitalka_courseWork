using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace chitalka_courseWork.MVVM.View
{

    public partial class AddingBookManuallyDialog : Window, INotifyPropertyChanged
    {
        private string _coverImagePath;

        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
        public string CoverImagePath
        {
            get => _coverImagePath;
            set
            {
                if (_coverImagePath != value)
                {
                    _coverImagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddingBookManuallyDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BookTitle = TitleTextBox.Text;
            Author = AuthorTextBox.Text;
            Description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(BookTitle) || string.IsNullOrEmpty(Author)) 
            {
                MessageBox.Show("Author or title may be empty.");
            }

            if (int.TryParse(PagesAmountTextBox.Text, out int pages) && pages > 0)
            {
                PagesCount = pages;
                if (!string.IsNullOrEmpty(BookTitle) && !string.IsNullOrEmpty(Author))
                    DialogResult = true;
            }
            else 
            {
                MessageBox.Show("Incorrect pages count input.");
                return;
            }

            
        }

        private void ChooseCoverButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                CoverImagePath = openFileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
