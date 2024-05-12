using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using chitalka_courseWork.MVVM.View;

namespace chitalka_courseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        { 

            InitializeComponent();
            Loaded += MainWindow_Loaded;
            searchBar.KeyDown += SearchBar_KeyDown;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGreeting();
        }

        private void UpdateGreeting()
        {
            int hour = DateTime.Now.Hour;

            if (hour >= 4 && hour < 12)
                greeting.Text =  "Доброго ранку, User!";
            else if (hour >= 12 && hour < 18)
                greeting.Text = "Добрий день, User!";
            else
                greeting.Text = "Добрий вечір, User!";
        }

        private void modeSwitcher_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private async void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = sender as TextBox;

                ControlTemplate template = textBox.Template;

                TextBox innerTextBox = (TextBox)template.FindName("SearchBox", textBox);

                if (!string.IsNullOrEmpty(innerTextBox.Text))
                {
                    string inputValue = innerTextBox.Text;

                    Task<List<Book>> taskSearchResult = BookSearch.Search(inputValue);
                    List<Book> books = await taskSearchResult;

                    bookListXAML.ItemsSource = books;

                    bookListXAML.Items.Refresh();
                }
            }
        }
    }
}