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

        private double amount = 1;
        private double credits = 100;
        private Random random = new Random();
        private Rectangle chicken;
        private int chickenSpeed = 40;
        private DispatcherTimer gameTimer;
        private double multiplier = 1;
        private double[] multipliers = {};
        private int chickenCordinate = 100 + (800 / 20 / 4) - (800 / 20);
        private int chickenStarCordinate = 100 + (800 / 20 / 4) - (800 / 20);
        private int stepsForward = 0;
        int steps = 2;


        public ChickenCross()
        {
            steps = random.Next(2, 22);
            InitializeComponent();
            UpdateBetDisplay();
            BackgroundCreation();
            InitializeGame();

        }

        private void InitializeGame()
        {
            if (credits <= 0)
            {
                MessageBox.Show("You're out of credits!", "Game Over");
                return;
            }
            chicken = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Yellow
            };

            Canvas.SetLeft(chicken, chickenStarCordinate);
            Canvas.SetTop(chicken, Height/2);
            gameCanvas.Children.Add(chicken);

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (stepsForward > steps)
            {
                CheckRestratGame();
                return;
            }
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
        private void CheckRestratGame()
        {
            credits -= amount;
            UpdateCreditsDisplay();
            if (stepsForward > steps) { MessageBox.Show("Game Over"); }
            Canvas.SetLeft(chicken, chickenStarCordinate);
            Canvas.SetTop(chicken, (Height - 50) / 2);
            stepsForward = 0;
            multiplier = 1;
            chickenCordinate = chickenStarCordinate;
            BackgroundCreation();
            steps = random.Next(2, 22);
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
                multiplier = (multiplier + (i * 0.1))/1.2;
                Array.Resize(ref multipliers, multipliers.Length + 1);
                multipliers[multipliers.Length - 1] = multiplier;
                var lable = new Label()
                {
                    Content = Math.Round(multiplier, 2),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                //lable.Name = $"lbl{i}";
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

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            int cordinate = (chickenCordinate - chickenStarCordinate) / chickenSpeed;
            credits += Math.Round(multipliers[cordinate] * amount, 2);
            CheckRestratGame();
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
            amount = amount -= 1;
            UpdateBetDisplay();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount += 1;
            UpdateBetDisplay();
        }
    }
}
