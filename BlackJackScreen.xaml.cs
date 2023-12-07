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

        //public Cards card;

        public BlackJackScreen(CardMechanics cardMechanics)
        {
            InitializeComponent();
            SetCardMechanic(cardMechanics);
            Cards card;
            HighScoreScreen highScoreScreen;
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
            CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
        }

        
        public void DealerHand()
        {
            Cards card = cardMechanics.DealCardDealer();

            FirstCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
        }
        private void DealCardButton_Click(object sender, RoutedEventArgs e)
        {
            UserHand();
            DealerHand();
            cardMechanics.CheckBlackJack();
            DealCardButton.IsEnabled = false;
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            
            cardMechanics.Hit();
            cardMechanics.CheckBust();
            UpdatePlayAgainButtonVisibility();
            if (cardMechanics.CheckBust() == false) 
            {
                Cards card3 = cardMechanics.DealCardUser();
                ThirdCardImageUser.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));
                //if (ThirdCardImageUser.Source != null)
                //{
                //    Cards card4 = cardMechanics.DealCardUser();
                //    FourthCardImageUser.Source = new BitmapImage(new Uri(card4.ImagePathFront, UriKind.Relative));
                //}
            }
            

            


        }
            private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            cardMechanics.Stand();
            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
            UpdatePlayAgainButtonVisibility();

        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            cardMechanics.NewRound();

            FirstCardImageUser.Source = null;
            FirstCardImageDealer.Source = null;
            SecondCardImageUser.Source = null;
            ThirdCardImageUser.Source = null;
            FourthCardImageUser.Source = null;


            CardTotalUserLabel.Content = "0";
            CardTotalDealerLabel.Content = "0";

            DealCardButton.IsEnabled = true;
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;

            PlayAgainButton.Visibility = Visibility.Hidden;
        }
        private void UpdatePlayAgainButtonVisibility()
        {
            if (cardMechanics.isGameFinished == true)
            {
                PlayAgainButton.Visibility = Visibility.Visible;
            }
            else
            {
                PlayAgainButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
