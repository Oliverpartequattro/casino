using System;
using System.Collections.Generic;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly string csvFilePath = "users.csv";

        // Static property to store the current user
        public static string CurrentUser { get; private set; }

        public Login()
        {
            InitializeComponent();
            if (!File.Exists(csvFilePath))
            {
                File.WriteAllText(csvFilePath, "userName;registrationDate;age;password\n");
            }
        }

        public Login(string currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterUsername.Text;
            string ageText = RegisterAge.Text;
            string password = RegisterPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(ageText) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(ageText, out int age))
            {
                MessageBox.Show("Age must be a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string newUserLine = $"{username};{DateTime.Now:yyyy-MM-dd};{age};{password};10000";
            File.AppendAllText(csvFilePath, newUserLine + "\n");
            MessageBox.Show("Registration successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text;
            string password = LoginPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] users = File.ReadAllLines(csvFilePath);
            foreach (var user in users)
            {
                string[] parts = user.Split(';');
                if (parts[0] == username && parts[3] == password)
                {
                    // Store the current user when login is successful
                    CurrentUser = username;

                    MessageBox.Show("Login successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
