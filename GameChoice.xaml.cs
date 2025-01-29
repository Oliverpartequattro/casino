using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.ShowDialog();
        }

        private void btnRoulette_Click(object sender, RoutedEventArgs e)
        {
            Roulette roulette = new Roulette();
            roulette.ShowDialog();
        }
    }
}
