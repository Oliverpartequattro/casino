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
        private User currentUser = Login.CurrentUser;
        private int balance;
        private int betAmount;
        private Random r = new Random();

        public BlackJack()
        {
            InitializeComponent();
            allCards = InitCards();
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            balance = currentUser.Balance;
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
                Functions.changeBalance(betAmount * -1, currentUser);
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
                Functions.AddImageStackPanel(playerCardsPanel, 0, 0, card.Img, 50, 75, "", 5, 5, 5, 5);
            }

            foreach (Card card in dealerCards)
            {
                Functions.AddImageStackPanel(dealerCardsPanel, 0, 0, card.Img, 50, 75, "", 5, 5, 5, 5);
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
                new ErrorBox("Túlment 21-en!", "Bust", true).ShowDialog();
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
                Functions.changeBalance(betAmount * -1, currentUser);
                message = "Túlment 21-en! Új játék?";
                title = "Vereség";
            }
            else if (dealerFinalScore > 21 || playerFinalScore > dealerFinalScore)
            {
                balance += betAmount * 2;
                Functions.changeBalance(betAmount * 2, currentUser);
                message = "Dealer veszített! Új játék?";
                title = "Győzelem";
            }
            else if (dealerFinalScore > playerFinalScore)
            {
                balance -= betAmount;
                Functions.changeBalance(betAmount * -1, currentUser);
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
            int cardIndex = r.Next(0, allCards.Count);
            return allCards[cardIndex];
        }

        private void EndGame(string message, string title)
        {
            ErrorBox questionBox = new ErrorBox(message, title, false, true);
            questionBox.ShowDialog();  

            if (questionBox.Answer)
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
                new ErrorBox("Elfogyott a pénzed, játék vége!", "Olcsó János", true).ShowDialog();
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
