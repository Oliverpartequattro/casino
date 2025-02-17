using System;
using System.Windows;
using System.Windows.Media;

namespace CasinoSimulator
{
    public partial class ErrorBox : Window
    {
        public ErrorBox(string message, string title, bool isError)
        {
            InitializeComponent();
            ErrorMessage.Text = message;
            this.Title = title;
            this.btn.Content = "Sajnálom";

            if (!isError)
            {
                this.g.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50"));
                this.btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5733"));
                this.btn.Content = "Köszönöm";

            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
