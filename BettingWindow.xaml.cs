using System.Windows;

namespace CasinoSimulator 
{
    public partial class BettingWindow : Window
    {
        public int BetAmount { get; set; }
        private int balance;

        public BettingWindow(int currentBalance)
        {
            InitializeComponent();
            balance = currentBalance;
            txtBalance.Text = $"Egyenleg: {balance.ToString()}";
            txtBet.Focus();
        }

        private void btnBet_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtBet.Text, out int bet) && bet > 0 && bet <= balance)
            {
                BetAmount = bet;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hibás Tét! A Tét 0-nál nagyobb, de az egyenlegedtől kisebb kell hogy legyen.", "Téti hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBet.Clear();
                txtBet.Focus();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}