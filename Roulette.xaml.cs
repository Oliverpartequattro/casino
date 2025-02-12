using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CasinoSimulator
{
    public partial class Roulette : Window
    {
        private int balance = 2800;
        private int currentBet = 0;
        private readonly Random random = new Random();
        private Button selectedBetButton;
        private Ellipse ball;

        public Roulette()
        {
            InitializeComponent();
            DrawRouletteWheel();
            DrawBettingTable();
        }

        private void DrawRouletteWheel()
        {
            //string[] numbers = { "0", "32", "15", "19", "4", "21", "2", "25", "17", "34", "6", "27", "13", "36", "11",
            //             "30", "8", "23", "10", "5", "24", "16", "33", "1", "20", "14", "31", "9", "22", "18",
            //             "29", "7", "28", "12", "35", "3", "26" };

            string[] numbers = { "6", "27", "13", "36", "11", "30", "8", "23", "10", "5", "24", "16", "33", "1", "20",
                         "14", "31", "9", "22", "18", "29", "7", "28", "12", "35", "3", "26", "0", "32", "15",
                         "19", "4", "21", "2", "25", "17", "34" };

            //string[] colors = { "Green", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red",
            //            "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black",
            //            "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black" };

            string[] colors = { "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red",
                        "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black",
                        "Red", "Black", "Red", "Black", "Green", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red" };




            double angleStep = 360.0 / numbers.Length;
            double radius = 200;

            // Create a container for the entire wheel
            var wheelContainer = new Canvas
            {
                Width = 2 * radius,
                Height = 2 * radius
            };

            for (int i = 0; i < numbers.Length; i++)
            {
                double startAngle = i * angleStep;
                double endAngle = (i + 1) * angleStep;

                // Convert angles to radians
                double startRad = Math.PI * startAngle / 180;
                double endRad = Math.PI * endAngle / 180;

                // Define start and end points for the segments
                Point center = new Point(radius, radius);
                Point startPoint = new Point(center.X + Math.Cos(startRad) * radius, center.Y + Math.Sin(startRad) * radius);
                Point endPoint = new Point(center.X + Math.Cos(endRad) * radius, center.Y + Math.Sin(endRad) * radius);

                // Create the segment path
                var pathFigure = new PathFigure
                {
                    StartPoint = center,
                    Segments = new PathSegmentCollection
            {
                new LineSegment(startPoint, true),
                new ArcSegment(endPoint, new Size(radius, radius), 0, false, SweepDirection.Clockwise, true)
            }
                };

                var path = new Path
                {
                    Fill = (Brush)new BrushConverter().ConvertFromString(colors[i]),
                    Data = new PathGeometry(new[] { pathFigure })
                };

                wheelContainer.Children.Add(path);

                // Add numbers to the wheel
                double textAngle = startAngle + angleStep / 2; // Position number in the center of the segment
                double textX = center.X + Math.Cos(Math.PI * textAngle / 180) * (radius - 20); // Slightly inside the wheel
                double textY = center.Y + Math.Sin(Math.PI * textAngle / 180) * (radius - 20);

                TextBlock numberText = new TextBlock
                {
                    Text = numbers[i],
                    Foreground = Brushes.White,
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Position the text element
                Canvas.SetLeft(numberText, textX - 15); // Adjust for text width
                Canvas.SetTop(numberText, textY - 10); // Adjust for text height

                wheelContainer.Children.Add(numberText);
            }

            // Add ball (existing code)
            ball = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = Brushes.White,
            };
            Canvas.SetLeft(ball, 185);
            Canvas.SetTop(ball, 50);
            ballContainer.Children.Add(ball);

            // Apply rotation transform to the wheel container
            var rotateTransform = new RotateTransform();
            wheelContainer.RenderTransform = rotateTransform;

            // Add the wheel container to the canvas
            rouletteWheel.Children.Add(wheelContainer);
        }

        private void DrawBettingTable()
        {
            bettingTable.Children.Clear();
            bettingTable.RowDefinitions.Clear();
            bettingTable.ColumnDefinitions.Clear();

            for (int i = 0; i < 3; i++)
                bettingTable.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < 13; i++)
                bettingTable.ColumnDefinitions.Add(new ColumnDefinition());

            string[] numbers = { "3", "2", "1", "6", "5", "4", "9", "8", "7", "12", "11", "10", "15", "14", "13", "18", "17", "16", "21", "20", "19", "24", "23", "22", "27", "26", "25", "30", "29", "28", "33", "32", "31", "36", "35", "34", "0"};
            string[] colors = { "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Red", "Black", "Black", "Black", "Red", "Black", "Red", "Black", "Red", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Black", "Red", "Red", "Black", "Black", "Black", "Red", "Black", "Red", "Black", "Red", "Green"};

            for (int i = 0; i < numbers.Length; i++)
            {
                Button betButton = new Button
                {
                    Content = numbers[i],
                    Background = (Brush)new BrushConverter().ConvertFromString(colors[i]),
                    Foreground = Brushes.White,
                    FontSize = 30,
                    Margin = new Thickness(2)
                };

                Grid.SetRow(betButton, i % 3);
                Grid.SetColumn(betButton, i / 3);
                betButton.Click += BetButton_Click;
                bettingTable.Children.Add(betButton);
            }
        }

        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBetButton != null)
                selectedBetButton.BorderBrush = null;

            selectedBetButton = sender as Button;
            selectedBetButton.BorderBrush = Brushes.Yellow;
            selectedBetButton.BorderThickness = new Thickness(2);
        }

        private void PlaceBet_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBetButton == null || balance <= 0)
            {
                MessageBox.Show("Please select a valid bet or check your balance!");
                return;
            }

            currentBet += 100; // Example fixed bet
            balance -= 100;
            UpdateUI();
        }

        private void StartWheel_Click(object sender, RoutedEventArgs e)
        {
            // Reset the wheel and ball position
            var rotateTransform = new RotateTransform();
            rouletteWheel.RenderTransform = rotateTransform;
            rouletteWheel.RenderTransformOrigin = new Point(0.5, 0.5);

            // Define the numbers on the wheel
            string[] numbers = { "6", "27", "13", "36", "11", "30", "8", "23", "10", "5", "24", "16", "33", "1", "20",
                         "14", "31", "9", "22", "18", "29", "7", "28", "12", "35", "3", "26", "0", "32", "15",
                         "19", "4", "21", "2", "25", "17", "34" };

            int currentStep = 0;

            // Pre-calculate the winning number
            int winningNumberIndex = random.Next(0, 37);
            string winningNumber = numbers[winningNumberIndex];
            // Calculate the target rotation angle
            double angleStep = 360.0 / numbers.Length;
            double targetAngle = int.Parse(winningNumber) * angleStep;
            double currentAngle = currentStep * angleStep;
            double totalRotation = 360 * 5 + (360 - currentAngle) + targetAngle; // Add multiple spins for effect

            // Define the spin animation
            var animation = new DoubleAnimation(0, totalRotation, new Duration(TimeSpan.FromSeconds(5)))
            {
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
            };

            // Start the wheel spin animation
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);

            // Handle animation completion
            animation.Completed += (s, _) =>
            {
                // Display the winning number
            };
                MessageBox.Show($"Winning number is: {winningNumber}");


            if (selectedBetButton != null && selectedBetButton.Content.ToString() != null && int.Parse(selectedBetButton.Content.ToString()) == int.Parse(winningNumber))
            {
                balance += (currentBet * 36);
            }
            else { currentBet = 0; }
            UpdateUI();
            //currentStep = int.Parse(numbers[winningNumberIndex]);
        }


        private void UpdateUI()
        {
            currentBetText.Text = $"Current Bet: {currentBet}";
            balanceText.Text = $"Balance: {balance}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
