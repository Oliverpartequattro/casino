using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CasinoSimulator
{
    /// <summary>
    /// Interaction logic for ChickenCross.xaml
    /// </summary>
    public partial class ChickenCross : Window
    {
        

        private Rectangle chicken;
        private double chickenSpeed = 800/20;
        private DispatcherTimer gameTimer;

        public ChickenCross()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Initialize the chicken
            chicken = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Yellow
            };


            // Set initial position of the chicken
            Canvas.SetLeft(chicken,100 + (800/20/4) - (800 / 20));
            Canvas.SetTop(chicken, gameCanvas.Height/2);
            gameCanvas.Children.Add(chicken);

            // Set up a game timer to update the game state
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            // Handle key presses
            this.KeyDown += MainWindow_KeyDown;
        }

        // Update the game state in each frame
        private void GameLoop(object sender, EventArgs e)
        {
            // For now, nothing happens, but this is where you can add animations and game logic
        }

        // Handle key presses to move the chicken
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(chicken);
            double y = Canvas.GetTop(chicken);
            bool a = x < (gameCanvas.Width - chicken.Width);
            if (e.Key == Key.Right && x < (gameCanvas.Width - 100) - chicken.Width)
                Canvas.SetLeft(chicken, x + chickenSpeed);
        }

    }
}
