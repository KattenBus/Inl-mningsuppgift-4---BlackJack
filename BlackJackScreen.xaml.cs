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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for BlackJackScreen.xaml
    /// </summary>
    public partial class BlackJackScreen : Window
    {
        CardMechanics cardMechanics;
        public BlackJackScreen(CardMechanics cardMechanics)
        {
            InitializeComponent();
            SetCardMechanic(cardMechanics);
            
        }
        public void SetCardMechanic(CardMechanics cardmechanics)
        {
            this.cardMechanics = cardmechanics;
        }
        public void UserHand()
        {
            Cards card = cardMechanics.DealCardUser();
            FirstCardImageUser.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            Cards card2 = cardMechanics.DealCardUser();
            SecondCardImageUser.Source = new BitmapImage(new Uri(card2.ImagePathFront, UriKind.Relative));

            if ((card.ID == "Hearts Ace" || card.ID == "Spades Ace" ||
                card.ID == "Diamonds Ace" || card.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
            {
                card.Value = 1;
            }

            CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
        }
        public void DealerHand()
        {
            Cards card = cardMechanics.DealCardDealer();
            FirstCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            SecondCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathBack, UriKind.Relative));

            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
        }
        private void DealCardButton_Click(object sender, RoutedEventArgs e)
        {
            UserHand();
            DealerHand();
            cardMechanics.CheckBlackJack();
            UpdatePlayAgainButtonVisibility();
            DealCardButton.IsEnabled = false;
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThirdCardImageUser.Source == null)
            {
                Cards card3 = cardMechanics.DealCardUser();
                ThirdCardImageUser.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" || 
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card3.Value = 1;
                }
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
            else if (FourthCardImageUser.Source == null)
            {
                Cards card4 = cardMechanics.DealCardUser();
                FourthCardImageUser.Source = new BitmapImage(new Uri(card4.ImagePathFront, UriKind.Relative));

                if ((card4.ID == "Hearts Ace" || card4.ID == "Spades Ace" ||
                    card4.ID == "Diamonds Ace" || card4.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card4.Value = 1;
                }

                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            else if (FifthCardImageUser.Source == null)
            {
                Cards card5 = cardMechanics.DealCardUser();
                FifthCardImageUser.Source = new BitmapImage(new Uri(card5.ImagePathFront, UriKind.Relative));

                if ((card5.ID == "Hearts Ace" || card5.ID == "Spades Ace" ||
                    card5.ID == "Diamonds Ace" || card5.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card5.Value = 1;
                }
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
        }
        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            while (cardMechanics.CalculateHandValueDealer() <= 16)
            {
                if (cardMechanics.DealerCards.Count < 2)
                {
                    Cards card2 = cardMechanics.DealCardDealer();
                    SecondCardImageDealer.Source = new BitmapImage(new Uri(card2.ImagePathFront, UriKind.Relative));

                    if ((card2.ID == "Hearts Ace" || card2.ID == "Spades Ace" ||
                        card2.ID == "Diamonds Ace" || card2.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card2.Value = 1;
                    }

                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();

                }
                else if (ThirdCardImageDealer.Source == null)
                {
                    Cards card3 = cardMechanics.DealCardDealer();
                    ThirdCardImageDealer.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                    if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" ||
                        card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card3.Value = 1;
                    }

                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();
                }
                else if (FourthCardImageDealer.Source == null)
                {
                    Cards card4 = cardMechanics.DealCardDealer();
                    FourthCardImageDealer.Source = new BitmapImage(new Uri(card4.ImagePathFront, UriKind.Relative));

                    if ((card4.ID == "Hearts Ace" || card4.ID == "Spades Ace" ||
                        card4.ID == "Diamonds Ace" || card4.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card4.Value = 1;
                    }

                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();
                }
            }
            cardMechanics.DealersTurn();
            cardMechanics.CheckBlackJackDealer();
            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
            UpdatePlayAgainButtonVisibility();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

        }



        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            cardMechanics.NewRound();

            FirstCardImageUser.Source = null;
            SecondCardImageUser.Source = null;
            ThirdCardImageUser.Source = null;
            FourthCardImageUser.Source = null;
            FifthCardImageUser.Source = null;
            FirstCardImageDealer.Source = null;
            SecondCardImageDealer.Source = null;
            ThirdCardImageDealer.Source = null;
            FourthCardImageDealer.Source = null;
            FifthCardImageDealer.Source = null;
            CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();

            DealCardButton.IsEnabled = true;
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
            cardMechanics.isGameFinished = false;
            UpdatePlayAgainButtonVisibility();
        }
        private void UpdatePlayAgainButtonVisibility()
        {
            if (cardMechanics.isGameFinished == true)
            {
                PlayAgainButton.Visibility = Visibility.Visible;
                HitButton.IsEnabled = false;
                StandButton.IsEnabled = false;
            }
            else
            {
                PlayAgainButton.Visibility = Visibility.Hidden;
            }
        }
        

    }

}
