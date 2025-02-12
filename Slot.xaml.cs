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

namespace CasinoSimulator
{
    
    public partial class Slot : Window
    {
        private Random random = new Random();
        private int credits = 100;
        private int screenSize = 800;
        private int amount = 1;
        private int loseCounter = 0;


        public Slot()
        {
            InitializeComponent();
            BackgroundApear();
            UpdateCreditsDisplay();
            UpdateBetDisplay();
        }

        private async void SpinButton_Click(object sender, RoutedEventArgs e)
        {

            
            if (credits <= 0)
            {
                MessageBox.Show("You're out of credits! Reset the game to play again.", "Game Over");
                return;
            }
            
            credits -= amount; 
            UpdateCreditsDisplay();

            SpinButton.IsEnabled = false;

            await SpinReel(Border1, Reel1);
            await SpinReel(Border2, Reel2);
            await SpinReel(Border3, Reel3);

            if (Reel1.Text == Reel2.Text && Reel2.Text == Reel3.Text)
            {
                int winnings = amount * 50;
                credits += winnings;
                MessageBox.Show($"Congratulations! You won {winnings} credits!", "Winner!");
                UpdateCreditsDisplay();
            }
            else
            {
                loseCounter++;
            }

            SpinButton.IsEnabled = true;
        }

        private async Task SpinReel(Border borderNum, TextBlock reel)
        {
            
            List<string> kepek = new List<string>()
            {
                { "seven.jpg" },
                { "cherry.jpg" },
                { "plum.jpg" },
                { "bell.jpg" },
                { "orange.jpg" },
            };
            for (int i = 0; i < 10; i++)
            {
                var kep = kepek[random.Next(kepek.Count())];

                var path = System.IO.Path.Combine(Environment.CurrentDirectory, "img/slot", kep);
                ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));
                borderNum.Background = new ImageBrush(src);
                reel.Text = kep;
                await Task.Delay(100); // Delay for animation effect



            }
        }
        private void BackgroundApear()
        {
            var kep = "slotmachinefurit.jpg";
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "img", kep);
            ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));
            this.Background = new ImageBrush(src);
        }

        private void UpdateCreditsDisplay()
        {
            CreditsText.Text = $"Credits: {credits}";
        }
        private void UpdateBetDisplay()
        {
            BetText.Text = $"Bet: {amount}";
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount-=1;

            UpdateBetDisplay();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount+=1;
            UpdateBetDisplay();
        }
    }
    
}
