using System.Windows;

namespace chitalka_courseWork.MVVM.View
{
    public partial class ConfirmationWindow : Window
    {
        public bool IsYesChoice;

        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            IsYesChoice = true;
            this.DialogResult = true; 
            this.Close(); 
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            IsYesChoice = false;
            this.DialogResult = false; 
            this.Close(); 
        }
    }
}
