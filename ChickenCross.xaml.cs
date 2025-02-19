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
                { "talca.jpg" },
        };

        private User currentUser = Login.CurrentUser;
        private double amount = 1;
        private double credits = 0;
        private Random random = new Random();
        private Rectangle chicken;
        private int chickenSpeed = 40;
        private DispatcherTimer gameTimer;
        private double multiplier = 0.25;
        private double[] multipliers = {};
        private int chickenCordinate = 100 + (800 / 20 / 4) - (800 / 20)-10;
        private int chickenStarCordinate = 100 + (800 / 20 / 4) - (800 / 20)-10;
        private int stepsForward = 0;
        int steps = 2;


        public ChickenCross()
        {
            steps = random.Next(2, 22);
            InitializeComponent();
            credits = currentUser.Balance;
            UpdateBetDisplay();
            UpdateCreditsDisplay();
            BackgroundCreation();
            InitializeGame();

        }

        private void InitializeGame()
        {
            
            
            chicken = new Rectangle
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Yellow
            };

            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "img/chicken", "Chicken.png");
            ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));
            chicken.Fill = new ImageBrush(src);

            Canvas.SetLeft(chicken, chickenStarCordinate);
            Canvas.SetTop(chicken, Height/2);
            gameCanvas.Children.Add(chicken);
            
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (credits <= 0)
            {
                new ErrorBox("Nincsen pénzed!", "Game Over", true).ShowDialog();
                return;
            }
            if (credits < amount)
            {
                new ErrorBox("Nincsen elég pénzed!", "Bet Error", true).ShowDialog();
                return;
            }
            double x = Canvas.GetLeft(chicken);
            double y = Canvas.GetTop(chicken);
            bool a = x < (gameCanvas.Width - chicken.Width);
            if (e.Key == Key.Right && x < (gameCanvas.Width - 80) - chicken.Width)
            {
                Canvas.SetLeft(chicken, x + chickenSpeed);
                chickenCordinate += chickenSpeed;
                stepsForward += 1;
                BackgroundUpdate();
            }
            if (stepsForward > steps)
            {
                CheckRestratGame();
                return;
            }
            if (stepsForward == 21)
            {
                CheckRestratGame();
                return;
            }
        }
        private void CheckRestratGame()
        {
            credits -= amount;
            Functions.changeBalance((int)amount * -1, currentUser);
            UpdateCreditsDisplay();
            if (stepsForward == 21) { new ErrorBox($"Gratulálunk! Nyertél {amount * 10} creditet!", "Winner", false).ShowDialog(); credits += amount * 10; Functions.changeBalance((int)amount * 10, currentUser); }
            if (stepsForward > steps) { new ErrorBox($"Vesztettél", "Game Over", true).ShowDialog(); }
            Canvas.SetLeft(chicken, chickenStarCordinate);
            Canvas.SetTop(chicken, (Height - 50) / 2);
            stepsForward = 0;
            multiplier = 0.25;
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
                multiplier = multiplier * 1.2;
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
            if (cordinate < 1)
            {
                new ErrorBox("Lépned kell", "Step Error", true).ShowDialog(); ;
                return;
            }
            else
            {
                credits += multipliers[cordinate] * amount;
                new ErrorBox($"Gratulálunk!! Nyertél {Math.Round(multipliers[cordinate] * amount)} creditet.", "Step Error", false).ShowDialog();
            }
            CheckRestratGame();
        }

        private void UpdateCreditsDisplay()
        {
            CreditsText.Text = $"Credits: {Math.Round(credits, 2)}";
        }
        private void UpdateBetDisplay()
        {
            BetText.Text = $"Bet: {amount}";
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (amount <= 1)
            {
                new ErrorBox("Nem tudsz 0 tétet rakni","Bet error", true).ShowDialog();
            }
            else 
            { 
                amount = amount -= 1;
                UpdateBetDisplay();
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            amount = amount += 1;
            UpdateBetDisplay();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            GameChoice gCWindow = new GameChoice();
            gCWindow.Show();
            this.Close();
        }
    }
}
