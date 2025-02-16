using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoSimulator
{
    public partial class BlackJack : Window
    {
        private List<Card> dealerCards;
        private List<Card> playerCards;
        private List<Card> allCards;
        private int balance;
        private int betAmount;

        public BlackJack()
        {
            InitializeComponent();
            allCards = InitCards();
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            balance = 10000;
            BettingScreen();
            DealCards();
        }

        private List<Card> InitCards()
        {
            string[] rows = File.ReadAllLines("data/cards.csv");
            return rows.Select(row => new Card(row)).ToList();
        }

        private void BettingScreen()
        {
            BettingWindow bettingWindow = new BettingWindow(balance);
            if (bettingWindow.ShowDialog() == true)
            {
                betAmount = bettingWindow.BetAmount;
                balance -= betAmount; 
                txtBalance.Text = balance.ToString(); 
            }
            else
            {
                Close(); 
            }
        }

        private void DealCards()
        {
            playerCards.Add(DrawCard());
            playerCards.Add(DrawCard());
            dealerCards.Add(DrawCard());
            dealerCards.Add(DrawCard());
            UpdateUI();
        }

        private void UpdateUI()
        {
            txtPlayerPoints.Text = $"Pont: {CalculateScore(playerCards)}";
            txtDealerPoints.Text = $"Pont: {CalculateScore(dealerCards)}";
            if(balance <= 0)
            {
                txtBalance.Text = "0";
                DisplayCards();
            }
            else
            {
                txtBalance.Text = balance.ToString();
                DisplayCards();
            }

        }

        private void DisplayCards()
        {
            playerCardsPanel.Children.Clear();
            dealerCardsPanel.Children.Clear();

            foreach (Card card in playerCards)
            {
                ImageAdder.AddImageStackPanel(playerCardsPanel, 0, 0, card.Img, 50, 75, "", 5, 5, 5, 5);
            }

            foreach (Card card in dealerCards)
            {
                ImageAdder.AddImageStackPanel(dealerCardsPanel, 0, 0, card.Img, 50, 75, "", 5, 5, 5, 5);
            }
        }



        private int CalculateScore(List<Card> cards)
        {
            return cards.Sum(card => card.Value);
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            playerCards.Add(DrawCard());
            UpdateUI();
            if (CalculateScore(playerCards) > 21)
            {
                MessageBox.Show("Túlment 21-en!");
                DealerTurn();
            }
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            DealerTurn();
        }

        private void DealerTurn()
        {
            while (CalculateScore(dealerCards) < 17)
            {
                dealerCards.Add(DrawCard());
                UpdateUI();
            }
            DetermineWinner();
        }

        private void DetermineWinner()
        {
            int playerFinalScore = CalculateScore(playerCards);
            int dealerFinalScore = CalculateScore(dealerCards);
            string message;
            string title;

            if (playerFinalScore > 21)
            {
                balance -= betAmount;
                message = "Túlment 21-en! Új játék?";
                title = "Vereség";
            }
            else if (dealerFinalScore > 21 || playerFinalScore > dealerFinalScore)
            {
                balance += betAmount * 2;
                message = "Dealer veszített! Új játék?";
                title = "Győzelem";
            }
            else if (dealerFinalScore > playerFinalScore)
            {
                balance -= betAmount;
                message = "Dealer nyert! Új játék?";
                title = "Vereség";
            }
            else
            {
                message = "Döntetlen! Új játék?";
                title = "Döntetlen";
            }

            txtBalance.Text = balance.ToString();
            EndGame(message, title);
        }

        private Card DrawCard()
        {
            var r = new Random();
            int cardIndex = r.Next(0, allCards.Count);
            return allCards[cardIndex];
        }

        private void EndGame(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                GameChoice gCWindow = new GameChoice();
                gCWindow.Show();
                this.Close();
            }
        }

        private void ResetGame()
        {
            if (balance <= 0)
            {
                MessageBox.Show("Elfogyott a pénzed!", "Játék vége", MessageBoxButton.OK, MessageBoxImage.Warning);
                GameChoice gCWindow = new GameChoice();
                gCWindow.Show();
                this.Close();
                return;
            }
            playerCards.Clear();
            dealerCards.Clear();
            DealCards();
        }

        private void BtnNewBet_Click(object sender, RoutedEventArgs e)
        {
            BettingScreen();
        }
    }
}
