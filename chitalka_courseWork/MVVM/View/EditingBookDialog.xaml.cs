using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chitalka_courseWork.MVVM.View
{

    public partial class EditingBookDialog : Window
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PagesCount { get; set; }

        public string SelectedEditingProperty { get; set; }

        public EditingBookDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SelectedEditingProperty);
        }
    }
}
