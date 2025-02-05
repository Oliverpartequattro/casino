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
            addImg(0, 0, "img/blackjack.png", 100, 100);
            addImg(0, 1, "img/blackjack.png", 100, 100);
            addImg(0, 2, "img/blackjack.png", 100, 100);
        }

        private void addImg(int row, int column, string path, int width, int height)
        {
            Image img = new Image();

            img.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            img.Width = width;
            img.Height = height;

            Grid.SetRow(img, row);
            Grid.SetColumn(img,column);

            img.Margin = new Thickness(5);

            g.Children.Add(img);
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
