using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        List<string> kepek = new List<string>()
        {
                { "start.jpg" },
                { "finish.jpg" },
                { "road.jpg" },
                { "roadwblock.jpg" },
        };

        private Random random = new Random();
        private Rectangle chicken;
        private int chickenSpeed = 40;
        private DispatcherTimer gameTimer;
        private double x = 1;
        private int chickenCordinate = 100 + (800 / 20 / 4) - (800 / 20);
        private int chickenStarCordinate = 100 + (800 / 20 / 4) - (800 / 20);
        private int stepsForward = 0;
        int steps = 2; //random.Next(2,22);


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


            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); 
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            
            this.KeyDown += MainWindow_KeyDown;
            
        }

        // Update the game state in each frame
        private void GameLoop(object sender, EventArgs e)
        {
            if (stepsForward > steps)
            {
                MessageBox.Show("Game Over");
                ChickenCross ckWindow = new ChickenCross();
                ckWindow.Show();
                return;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(chicken);
            double y = Canvas.GetTop(chicken);
            bool a = x < (gameCanvas.Width - chicken.Width);
            if (e.Key == Key.Right && x < (gameCanvas.Width - 100) - chicken.Width)
            {
                Canvas.SetLeft(chicken, x + chickenSpeed);
                chickenCordinate += chickenSpeed;
                stepsForward += 1;
                BackgroundUpdate();
            }
        }
        private void BackgroundUpdate()
        {
            int cordinate = (chickenCordinate - chickenStarCordinate) / chickenSpeed;
            if (cordinate >= 1 && cordinate < 21)
            {
                var border = new Border();

                var kep = kepek[3];
                var path = System.IO.Path.Combine(Environment.CurrentDirectory, "img/chicken", kep);
                ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));
                border.Background = new ImageBrush(src);

                Grid.SetColumn(border, cordinate);
                myGrid.Children.Add(border);
            }
        }

        private void BackgroundCreation()
        {

            

            for (int i = 0; i < 22; i++)
            {
                var kep = kepek[1];
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                x = (x + (i * 0.1))/1.2;
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
