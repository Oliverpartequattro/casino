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
    /// <summary>
    /// Interaction logic for Slot.xaml
    /// </summary>
    public partial class Slot : Window
    {
        private string[] symbols = { "🍒", "🍋", "🍊", "🔔", "⭐", "7" };
        private Random random = new Random();
        private int credits = 100;
        private int screenSize = 800;
        private int amount = 1;
        public Slot()
        {
            InitializeComponent();
            UpdateCreditsDisplay();
        }

        private async void SpinButton_Click(object sender, RoutedEventArgs e)
        {
            if (credits <= 0)
            {
                MessageBox.Show("You're out of credits! Reset the game to play again.", "Game Over");
                return;
            }
            
            credits -= amount; // Deduct one credit per spin
            UpdateCreditsDisplay();

            // Disable the spin button during the animation
            SpinButton.IsEnabled = false;

            // Simulate spinning animation
            await SpinReel(Reel1);
            await SpinReel(Reel2);
            await SpinReel(Reel3);

            // Check for a win
            if (Reel1.Text == Reel2.Text && Reel2.Text == Reel3.Text)
            {
                int winnings = 50;
                credits += winnings;
                MessageBox.Show($"Congratulations! You won {winnings} credits!", "Winner!");
                UpdateCreditsDisplay();
            }

            // Re-enable the spin button
            SpinButton.IsEnabled = true;
        }

        private async Task SpinReel(TextBlock reel)
        {
            for (int i = 0; i < 10; i++) // Spin 10 times
            {
                reel.Text = symbols[random.Next(symbols.Length)];
                await Task.Delay(100); // Delay for animation effect
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            credits = 100;
            Reel1.Text = "?";
            Reel2.Text = "?";
            Reel3.Text = "?";
            UpdateCreditsDisplay();
        }

        private void UpdateCreditsDisplay()
        {
            CreditsText.Text = $"Credits: {credits}";
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount--;
            UpdateCreditsDisplay();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount++;
            UpdateCreditsDisplay();
        }
    }
    
}
