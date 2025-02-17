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
                new ErrorBox("A Tétnek 0-nál nagyobbnak, de az egyenlegednél kisebbnek kell lennie!", "Téti hiba", true).ShowDialog();
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