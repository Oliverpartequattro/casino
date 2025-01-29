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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CasinoSimulator
{
    /// <summary>
    /// Interaction logic for Roulette.xaml
    /// </summary>
    public partial class Roulette : Window
    {
        private int currentBet = 0;
        private int balance = 2800;
        private readonly Random random = new Random();

        public Roulette()
        {
            InitializeComponent();
            DrawRouletteWheel();
        }

        private void DrawRouletteWheel()
        {
            // Define numbers and colors
            string[] numbers = { "0", "32", "15", "19", "4", "21", "2", "25", "17", "34", "6", "27", "13", "36", "11",
                     "30", "8", "23", "10", "5", "24", "16", "33", "1", "20", "14", "31", "9", "22", "18",
                     "29", "7", "28", "12", "35", "3", "26" };

            string[] colors = { "Green", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red",
                    "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black",
                    "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black" };

            double angleStep = 360.0 / numbers.Length;

            for (int i = 0; i < 37; i++)
            {
                Console.WriteLine($"Processing index {i}: Number = {numbers[i]}, Color = {colors[i]}");
                // Calculate the start and end angles for the section
                double startAngle = i * angleStep;
                double endAngle = (i + 1) * angleStep;

                // Convert angles to radians
                double startAngleRad = Math.PI * startAngle / 180;
                double endAngleRad = Math.PI * endAngle / 180;

                // Define points for the section
                Point center = new Point(150, 150); // Center of the wheel
                Point startPoint = new Point(center.X + Math.Cos(startAngleRad) * 150, center.Y + Math.Sin(startAngleRad) * 150);
                Point endPoint = new Point(center.X + Math.Cos(endAngleRad) * 150, center.Y + Math.Sin(endAngleRad) * 150);

                // Create a path for the section
                var section = new System.Windows.Shapes.Path
                {
                    Fill = (Brush)new BrushConverter().ConvertFromString(colors[i]),
                    Data = new PathGeometry(new[]
                    {
                new PathFigure(center, new PathSegmentCollection
                {
                    new LineSegment(startPoint, true),
                    new ArcSegment(endPoint, new Size(150, 150), 0, false, SweepDirection.Clockwise, true)
                }, true)
            })
                };

                // Add the section to the wheel
                rouletteWheel.Children.Add(section);
            }
        }

        private void Bet100_Click(object sender, RoutedEventArgs e)
        {
            if (balance >= 100)
            {
                currentBet += 100;
                balance -= 100;
                UpdateUI();
            }
        }

        private void StartWheel_Click(object sender, RoutedEventArgs e)
        {
            // Spin the wheel
            var rotateTransform = new RotateTransform();
            rouletteWheel.RenderTransform = rotateTransform;
            rouletteWheel.RenderTransformOrigin = new Point(0.5, 0.5);

            var animation = new DoubleAnimation(0, 360 * 5 + random.Next(0, 360), new Duration(TimeSpan.FromSeconds(3)))
            {
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            animation.Completed += (s, _) => CalculateResult();
        }

        private void CalculateResult()
        {
            // Calculate the winning number
            int winningNumber = random.Next(0, 37);
            MessageBox.Show($"Winning number is: {winningNumber}");

            // Check if the player won
            // Add logic for payouts here...
        }

        private void UpdateUI()
        {
            currentBetText.Text = $"Current Bet: {currentBet}";
            balanceText.Text = $"Current Balance: {balance}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
