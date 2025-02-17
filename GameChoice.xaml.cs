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
    /// Interaction logic for GameChoice.xaml
    /// </summary>
    public partial class GameChoice : Window
    {
        public GameChoice()
        {
            InitializeComponent();
            txtBalance.Text = Login.CurrentUser!.Balance.ToString() ?? "0";
            Functions.AddImageStackPanel(stackBlackjack, 0, 0, "blackjack.png", 100, 100, "img/", 0, 10, 0, 0);
            Functions.AddImageStackPanel(stackRoulette, 0, 0, "blackjack.png", 100, 100, "img/", 0, 10, 0, 0);
            Functions.AddImageStackPanel(stackSlot, 0, 0, "blackjack.png", 100, 100, "img/", 0, 10,0, 0);
            Functions.AddImageStackPanel(stackChicken, 0, 0, "blackjack.png", 100, 100, "img/", 0, 10, 0, 0);


        }

        private void btnBlackjack_Click(object sender, RoutedEventArgs e)
        {
            BlackJack blackJackWindow = new BlackJack();
            blackJackWindow.Show();

            this.Close();
        }

        private void btnCross_Click(object sender, RoutedEventArgs e)
        {
            ChickenCross ckWindow = new ChickenCross();
            ckWindow.Show();

            this.Close();
        }

        private void btnSlot_Click(object sender, RoutedEventArgs e)
        {
            Slot slotWindow = new Slot();
            slotWindow.Show();

            this.Close();
        }

        private void btnRoulette_Click(object sender, RoutedEventArgs e)
        {
            Roulette roulette = new Roulette();
            roulette.Show();
        }
    }
}
