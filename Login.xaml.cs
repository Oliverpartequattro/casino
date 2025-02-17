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

        private List<User> users;
        public static User CurrentUser { get; set; } = null;
        public Login()
        {
            users = InitUsers();
            InitializeComponent();
            CurrentUser = null;
        }

        private List<User> InitUsers()
        {
            string[] rows = File.ReadAllLines("data/users.csv");
            return rows.Select(row => new User(row)).ToList();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterUsername.Text;
            string ageTxt = RegisterAge.Text;
            string password = RegisterPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(ageTxt) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(ageTxt, out int age))
            {
                MessageBox.Show("Az életkornak egy számnak kell lennie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string newUserRow = $"{username};{age};{password};{1000}";

            if (!File.Exists("data/users.csv"))
            {
                using (StreamWriter sw = new StreamWriter("data/users.csv"))
                {
                    sw.WriteLine(newUserRow);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("data/users.csv", false))
                {
                    sw.WriteLine(newUserRow);
                }
            }

            MessageBox.Show("Sikeres regisztráció!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            RegisterUsername.Clear();
            RegisterAge.Clear();
            RegisterPassword.Clear();

            users = InitUsers();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text;
            string password = LoginPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    Login.CurrentUser = user;
                    MessageBox.Show("Sikeres bejelentkezés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    GameChoice gameChoiceWindow = new GameChoice();
                    gameChoiceWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

    }
}
