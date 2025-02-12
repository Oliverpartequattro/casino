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
        private int chickenCordinate = 100 + (800 / 20 / 4) - (800 / 20);

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
        private void BackgroundUpdate()
        {
            
        }

        private void BackgroundCreation()
        {

            List<string> kepek = new List<string>()
            {
                { "start.jpg" },
                { "finish.jpg" },
                { "road.jpg" },
                { "roadwblock.jpg" },
                { "orange.jpg" },
            };

            for (int i = 0; i < 22; i++)
            {
                double[] szorzok = []; 
                var kep = kepek[1];
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                x = (x + (i * 0.1))/1.2;
                szorzok[i] = x;
                var lable = new Label()
                {
                    Content = Math.Round(x, 2),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                var border = new Border();
               
                if (i < 21 && i > 0) { kep = kepek[2]; }
                if (i == 0) { kep = kepek[0]; myGrid.ColumnDefinitions[i].Width = new GridLength(100); };
                if (i == 21) { kep = kepek[1]; myGrid.ColumnDefinitions[i].Width = new GridLength(100); };

                    var path = System.IO.Path.Combine(Environment.CurrentDirectory, "img/chicken", kep);
                    ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));
                    border.Background = new ImageBrush(src);

                Grid.SetColumn(border, i);
                myGrid.Children.Add(border);
                if (i > 0 && i < 21 )
                {
                    myGrid.ColumnDefinitions[i].Width = new GridLength(800/20);
                    lable.Foreground = Brushes.White;
                    Grid.SetColumn(lable, i);
                    myGrid.Children.Add(lable);
                }
                
            }
        }
    }
}
