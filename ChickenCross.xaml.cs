using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private double x = 1;

        public ChickenCross()
        {
            InitializeComponent();
            BackgroundCreation();
            InitializeGame();

        }

        private void InitializeGame()
        {
            chicken = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Yellow
                


            };


            // Set initial position of the chicken
            Canvas.SetLeft(chicken,100 + (800/20/4) - (800 / 20));
            Canvas.SetTop(chicken, Height/2);
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
        private void BackgroundCreation()
        {
            for (int i = 0; i < 22; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                x = (x + (i * 0.1))/1.2;
                var lable = new Label()
                {
                    Content = Math.Round(x, 2),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                var border = new Border() { Background = Brushes.DimGray };
                var borderGreen = new Border() { Background = Brushes.DarkOliveGreen };
                if ((i % 2) == 0){
                    Grid.SetColumn(border, i);
                    myGrid.Children.Add(border);
                }
                if (i == 0 || i == 21)
                {
                    Grid.SetColumn(borderGreen, i);
                    myGrid.Children.Add(borderGreen);
                    myGrid.ColumnDefinitions[i].Width = new GridLength(100);
                }
                if (i > 0 && i < 21 )
                {
                    lable.Foreground = Brushes.White;
                    Grid.SetColumn(lable, i);
                    myGrid.Children.Add(lable);
                }
                
            }
        }

    }
}
