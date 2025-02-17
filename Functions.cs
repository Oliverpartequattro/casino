using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoSimulator
{
    public static class Functions
    {
        public static void AddImage(Grid grid, int row, int column, string imgName, int width, int height, string folder, double ml, double mt, double mr, double mb)
        {
            var path = Path.Combine(Environment.CurrentDirectory, folder, imgName);
            ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));

            Image img = new Image
            {
                Source = src,
                Width = width,
                Height = height,
                Margin = new Thickness(ml, mt, mr, mb)
            };

            Grid.SetRow(img, row);
            Grid.SetColumn(img, column);

            grid.Children.Add(img);
        }

        public static void AddImageStackPanel (StackPanel stackPanel, int row, int column, string imgName, int width, int height, string folder, double ml, double mt, double mr, double mb)
        {
            var path = Path.Combine(Environment.CurrentDirectory, folder, imgName);
            ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));

            Image img = new Image
            {
                Source = src,
                Width = width,
                Height = height,
                Margin = new Thickness(ml, mt, mr, mb)
            };

            stackPanel.Children.Add(img);
        }
        public static void AddImageWrapPanel(WrapPanel wrapPanel, int row, int column, string imgName, int width, int height, string folder, double ml, double mt, double mr, double mb)
        {
            var path = Path.Combine(Environment.CurrentDirectory, folder, imgName);
            ImageSource src = new BitmapImage(new Uri(path, UriKind.Absolute));

            Image img = new Image
            {
                Source = src,
                Width = width,
                Height = height,
                Margin = new Thickness(ml, mt, mr, mb)
            };

            wrapPanel.Children.Add(img);
        }

        public static void changeBalance(int bal, User modifiedUser)
        {
            User user = modifiedUser;
            user.Balance += bal;

            string[] rows = File.ReadAllLines("data/users.csv");

            using (StreamWriter sw = new StreamWriter("data/users.csv", false))
            {
                foreach (string row in rows)
                {
                    string[] data = row.Split(';');
                    if (data[0] == user.Username)
                    {
                        sw.WriteLine($"{user.Username};{user.Age};{user.Password};{user.Balance}");
                    }
                    else
                    {
                        sw.WriteLine(row);
                    }
                }
            }
        }
    }
}
