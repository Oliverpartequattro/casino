using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows;

namespace CasinoSimulator
{
    public partial class BlackJack : Window
    {
        private List<Card> dealerCards;
        private List<Card> playerCards;
        private List<Card> allCards;
        private int playerScore;
        private int dealerScore;

        public BlackJack()
        {
            InitializeComponent();
            allCards = InitCards(); 
            dealerCards = new List<Card>();
            playerCards = new List<Card>();

            DealCards();
        }

        private List<Card> InitCards()
        {
            string[] rows = File.ReadAllLines("data/cards.csv"); 
            return rows.Select(row => new Card(row)).ToList(); 
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

            txtPlayerCards.Text = "Játékos kártyák: " + string.Join(", ", playerCards.Select(c => c.Value));
            txtDealerCards.Text = "Dealer kártyák: " + string.Join(", ", dealerCards.Select(c => c.Value));
        }

        private int CalculateScore(List<Card> cards)
        {
            int score = 0;
            foreach (var card in cards)
            {
                score += card.Value;
            }
            return score;
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

            int playerFinalScore = CalculateScore(playerCards);
            int dealerFinalScore = CalculateScore(dealerCards);

            if (playerFinalScore > 21)
            {
                EndGame("Túlment 21-en! Új játék?", "Vereség");
            }
            else if (dealerFinalScore > 21 || playerFinalScore > dealerFinalScore)
            {
                EndGame("Dealer veszített! Új játék?", "Győzelem");

            }
            else if (dealerFinalScore > playerFinalScore)
            {
                EndGame("Dealer nyert! Új játék?", "Vereség");
            }
            else
            {
                EndGame("Döntetlen! Új játék?", "Vereség");

            }
        }

        private Card DrawCard()
        {
            var r = new Random();
            int cardIndex = r.Next(0, allCards.Count);
            return allCards[cardIndex];
        }

        private void EndGame(string message, string title)
        {
            string boxMessage = message;
            string boxTitle = title;

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
            playerCards.Clear();
            dealerCards.Clear();
            DealCards();
        }
    }
}
