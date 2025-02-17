using System;
using System.Windows;

namespace CasinoSimulator
{
    public partial class ErrorBox : Window
    {
        public ErrorBox(string message, string title, bool isError)
        {
            InitializeComponent();
            ErrorMessage.Text = message;
            this.Title = title;

            if (!isError)
            {
                this.ErrorMessage.Foreground = System.Windows.Media.Brushes.Green;
                this.btn.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
