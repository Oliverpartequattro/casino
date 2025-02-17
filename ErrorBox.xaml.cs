using System;
using System.Windows;
using System.Windows.Media;

namespace CasinoSimulator
{
    public partial class ErrorBox : Window
    {
        public bool Answer { get; private set; } = false; 

        public ErrorBox(string message, string title, bool isError, bool isQuestion = false)
        {
            InitializeComponent();
            ErrorMessage.Text = message;
            this.Title = title;

            if (isQuestion)
            {
                this.btnOk.Visibility = Visibility.Collapsed;
                this.btnYes.Visibility = Visibility.Visible;
                this.btnNo.Visibility = Visibility.Visible;
                this.g.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFA00B")); 
            }
            else
            {
                this.btnOk.Visibility = Visibility.Visible;
                this.btnYes.Visibility = Visibility.Collapsed;
                this.btnNo.Visibility = Visibility.Collapsed;
                this.btnNo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5733"));
                this.btnYes.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50"));



                if (isError)
                {
                    this.g.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5733")); 
                    this.btnOk.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50"));
                    this.btnOk.Content = "Sajnálom";
                }
                else
                {
                    this.g.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50")); 
                    this.btnOk.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5733"));
                    this.btnOk.Content = "Köszönöm";
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = false;
            this.Close();
        }
    }
}
