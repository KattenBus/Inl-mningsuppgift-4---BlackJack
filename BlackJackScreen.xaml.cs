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
        public bool UserHasSplitAndStood = false;
        public BlackJackScreen(CardMechanics cardMechanics)
        {
            InitializeComponent();
            SetCardMechanic(cardMechanics);
            
        }
        public void SetCardMechanic(CardMechanics cardmechanics)
        {
            this.cardMechanics = cardmechanics;
        }
        public void DealHandUser()
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
        public void DealHandDealer()
        {
            Cards card = cardMechanics.DealCardDealer();
            FirstCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            SecondCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathBack, UriKind.Relative));

            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
        }
        private void DealCardButton_Click(object sender, RoutedEventArgs e)
        {
            DealHandUser();
            DealHandDealer();
            cardMechanics.Split();
            cardMechanics.CheckBlackJack();
            if (cardMechanics.CanSplit == true)
            {
                SplitButton.IsEnabled = true;
            }
            if (cardMechanics.CheckBlackJackIsTrue == true)
            {
                UpdatePlayAgainButtonVisibility();
                DealCardButton.IsEnabled = false;
                HitButton.IsEnabled = false;
                StandButton.IsEnabled = false;
                DoubleButton.IsEnabled = false;
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            else
            {
                UpdatePlayAgainButtonVisibility();
                DealCardButton.IsEnabled = false;
                HitButton.IsEnabled = true;
                StandButton.IsEnabled = true;
                DoubleButton.IsEnabled = true;
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
        }
        private void DoubleButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardMechanics.UserHasSplit == false)
            {
                cardMechanics.Double();

                Cards card3 = cardMechanics.DealCardUser();
                ThirdCardImageUser.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" ||
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card3.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.CalculateHandValueUser() < 22)
                {
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            if (UserHasSplitAndStood == true)
            {
                cardMechanics.DoubleSplit();

                Cards card3 = cardMechanics.DealCardUserSplit();
                ThirdCardImageUserSplit.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" ||
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card3.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    cardMechanics.UserCardsSplit.Clear();
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
                PerformStandButton_ClickLogic();
            }
            else if (cardMechanics.UserHasSplit == true)
            {
                cardMechanics.Double();

                Cards card3 = cardMechanics.DealCardUser();
                ThirdCardImageUser.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" ||
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card3.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                }
                UserHasSplitAndStood = true;
                MessageBox.Show("Play your second hand");
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            //if (cardMechanics.isGameFinished == true)
            //{

            //}
            //else
            //{
            //    PerformStandButton_ClickLogic();
            //}

        }
        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserHasSplitAndStood == true)
            {
                UserCanAddCardsToNewHand();
            }
            else if (ThirdCardImageUser.Source == null)
            {
                Cards card3 = cardMechanics.DealCardUser();
                ThirdCardImageUser.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" || 
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card3.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22)
                {
                    UserHasSplitAndStood = true;
                    MessageBox.Show("Play your second hand");
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                }
                UpdatePlayAgainButtonVisibility();
                DoubleButton.IsEnabled = false;
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
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }

                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >=22)
                {
                    UserHasSplitAndStood = true;
                    MessageBox.Show("Play your second hand");
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                }
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
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }

                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22)
                {
                    UserHasSplitAndStood = true;
                    MessageBox.Show("Play your second hand");
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            else if (SixthCardImageUser.Source == null)
            {
                Cards card6 = cardMechanics.DealCardUser();
                SixthCardImageUser.Source = new BitmapImage(new Uri(card6.ImagePathFront, UriKind.Relative));

                if ((card6.ID == "Hearts Ace" || card6.ID == "Spades Ace" ||
                    card6.ID == "Diamonds Ace" || card6.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card6.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }

                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22)
                {
                    UserHasSplitAndStood = true;
                    MessageBox.Show("Play your second hand");
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    SixthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }
            else if (SeventhCardImageUser.Source == null)
            {
                Cards card7 = cardMechanics.DealCardUser();
                SeventhCardImageUser.Source = new BitmapImage(new Uri(card7.ImagePathFront, UriKind.Relative));

                if ((card7.ID == "Hearts Ace" || card7.ID == "Spades Ace" ||
                    card7.ID == "Diamonds Ace" || card7.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                {
                    card7.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUser() > 21 && cardMechanics.UserCards.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCards.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }

                CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                cardMechanics.CheckBust();
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22)
                {
                    UserHasSplitAndStood = true;
                    MessageBox.Show("Play your second hand");
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    SixthCardImageUser.Source = null;
                    SeventhCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;
            }

        }
        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserHasSplitAndStood == true)
            {
                PerformStandButton_ClickLogic();
            }
            else if (cardMechanics.UserHasSplit == true)
            {
                MessageBox.Show("Play you second hand!");
                UserHasSplitAndStood = true;
                StandButton.IsEnabled = true;
            }
            else
            {
                PerformStandButton_ClickLogic();
            }      
        }
        private void UserCanAddCardsToNewHand()
        {
            if (ThirdCardImageUserSplit.Source == null)
            {
                Cards card3 = cardMechanics.DealCardUserSplit();
                ThirdCardImageUserSplit.Source = new BitmapImage(new Uri(card3.ImagePathFront, UriKind.Relative));

                if ((card3.ID == "Hearts Ace" || card3.ID == "Spades Ace" ||
                    card3.ID == "Diamonds Ace" || card3.ID == "Clover Ace") && cardMechanics.CalculateHandValueUserSplit() > 21)
                {
                    card3.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                    cardMechanics.UserCardsSplit.Clear();
                    CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                DoubleButton.IsEnabled = false;
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
            else if (FourthCardImageUserSplit.Source == null)
            {
                Cards card4 = cardMechanics.DealCardUserSplit();
                FourthCardImageUserSplit.Source = new BitmapImage(new Uri(card4.ImagePathFront, UriKind.Relative));

                if ((card4.ID == "Hearts Ace" || card4.ID == "Spades Ace" ||
                    card4.ID == "Diamonds Ace" || card4.ID == "Clover Ace") && cardMechanics.CalculateHandValueUserSplit() > 21)
                {
                    card4.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                    FourthCardImageUserSplit.Source = null;
                    cardMechanics.UserCardsSplit.Clear();
                    CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
            else if (FifthCardImageUserSplit.Source == null)
            {
                Cards card5 = cardMechanics.DealCardUserSplit();
                FifthCardImageUserSplit.Source = new BitmapImage(new Uri(card5.ImagePathFront, UriKind.Relative));

                if ((card5.ID == "Hearts Ace" || card5.ID == "Spades Ace" ||
                    card5.ID == "Diamonds Ace" || card5.ID == "Clover Ace") && cardMechanics.CalculateHandValueUserSplit() > 21)
                {
                    card5.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                    FourthCardImageUserSplit.Source = null;
                    FifthCardImageUserSplit.Source = null;
                    cardMechanics.UserCardsSplit.Clear();
                    CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
            else if (SixthCardImageUserSplit.Source == null)
            {
                Cards card6 = cardMechanics.DealCardUserSplit();
                SixthCardImageUserSplit.Source = new BitmapImage(new Uri(card6.ImagePathFront, UriKind.Relative));

                if ((card6.ID == "Hearts Ace" || card6.ID == "Spades Ace" ||
                    card6.ID == "Diamonds Ace" || card6.ID == "Clover Ace") && cardMechanics.CalculateHandValueUserSplit() > 21)
                {
                    card6.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                    FourthCardImageUserSplit.Source = null;
                    FifthCardImageUserSplit.Source = null;
                    SixthCardImageUserSplit.Source = null;
                    cardMechanics.UserCardsSplit.Clear();
                    CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
            else if (SeventhCardImageUserSplit.Source == null)
            {
                Cards card7 = cardMechanics.DealCardUserSplit();
                SeventhCardImageUserSplit.Source = new BitmapImage(new Uri(card7.ImagePathFront, UriKind.Relative));

                if ((card7.ID == "Hearts Ace" || card7.ID == "Spades Ace" ||
                    card7.ID == "Diamonds Ace" || card7.ID == "Clover Ace") && cardMechanics.CalculateHandValueUserSplit() > 21)
                {
                    card7.Value = 1;
                }
                if (cardMechanics.CalculateHandValueUserSplit() > 21 && cardMechanics.UserCardsSplit.Any(card => card.ID.Contains("Ace")))
                {
                    var aceCard = cardMechanics.UserCardsSplit.First(card => card.ID.Contains("Ace"));
                    aceCard.Value = 1;
                }
                CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                cardMechanics.CheckBustSplit();
                if (cardMechanics.CalculateHandValueUserSplit() >= 22)
                {
                    FirstCardImageUserSplit.Source = null;
                    SecondCardImageUserSplit.Source = null;
                    ThirdCardImageUserSplit.Source = null;
                    FourthCardImageUserSplit.Source = null;
                    FifthCardImageUserSplit.Source = null;
                    SixthCardImageUserSplit.Source = null;
                    SeventhCardImageUserSplit.Source = null;
                    cardMechanics.UserCardsSplit.Clear();
                    CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
                    PerformStandButton_ClickLogic();
                }
                UpdatePlayAgainButtonVisibility();
                totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            }
        }
            public void PerformStandButton_ClickLogic()
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
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
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
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
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
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
                    }
                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();
                }
                else if (FifthCardImageDealer.Source == null)
                {
                    Cards card5 = cardMechanics.DealCardDealer();
                    FifthCardImageDealer.Source = new BitmapImage(new Uri(card5.ImagePathFront, UriKind.Relative));

                    if ((card5.ID == "Hearts Ace" || card5.ID == "Spades Ace" ||
                        card5.ID == "Diamonds Ace" || card5.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card5.Value = 1;
                    }
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
                    }
                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();
                }
                else if (SixthCardImageDealer.Source == null)
                {
                    Cards card6 = cardMechanics.DealCardDealer();
                    SixthCardImageDealer.Source = new BitmapImage(new Uri(card6.ImagePathFront, UriKind.Relative));

                    if ((card6.ID == "Hearts Ace" || card6.ID == "Spades Ace" ||
                        card6.ID == "Diamonds Ace" || card6.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card6.Value = 1;
                    }
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
                    }
                    CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();
                    //cardMechanics.CheckBustDealer();
                    UpdatePlayAgainButtonVisibility();
                }
                else if (SeventhCardImageDealer.Source == null)
                {
                    Cards card7 = cardMechanics.DealCardDealer();
                    SeventhCardImageDealer.Source = new BitmapImage(new Uri(card7.ImagePathFront, UriKind.Relative));

                    if ((card7.ID == "Hearts Ace" || card7.ID == "Spades Ace" ||
                        card7.ID == "Diamonds Ace" || card7.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
                    {
                        card7.Value = 1;
                    }
                    if (cardMechanics.CalculateHandValueDealer() > 21 && cardMechanics.DealerCards.Any(card => card.ID.Contains("Ace")))
                    {
                        var aceCard = cardMechanics.DealerCards.First(card => card.ID.Contains("Ace"));
                        aceCard.Value = 1;
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
            SixthCardImageUser.Source = null;
            SeventhCardImageUser.Source = null;
            FirstCardImageUserSplit.Source = null;
            SecondCardImageUserSplit.Source = null;
            ThirdCardImageUserSplit.Source = null;
            FourthCardImageUserSplit.Source = null;
            FifthCardImageUserSplit.Source = null;
            SixthCardImageUserSplit.Source = null;
            SeventhCardImageUserSplit.Source = null;
            FirstCardImageDealer.Source = null;
            SecondCardImageDealer.Source = null;
            ThirdCardImageDealer.Source = null;
            FourthCardImageDealer.Source = null;
            FifthCardImageDealer.Source = null;
            SixthCardImageDealer.Source = null;
            SeventhCardImageDealer.Source = null;
            CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
            CardTotalDealerLabel.Content = cardMechanics.CalculateHandValueDealer();


            DealCardButton.IsEnabled = true;
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
            DoubleButton.IsEnabled = false;
            cardMechanics.isGameFinished = false;
            cardMechanics.DoubleInitiated = false;
            cardMechanics.DoubleInitiatedSplit = false;
            cardMechanics.CanSplit = false;
            cardMechanics.UserHasSplit = false;
            SplitButton.IsEnabled = false;
            UserHasSplitAndStood = false;
            cardMechanics.CheckBlackJackSplitIsTrue = false;
            cardMechanics.CheckBlackJackIsTrue = false;
            CardTotalUserSplitLabel.Visibility = Visibility.Hidden;
            UpdatePlayAgainButtonVisibility();
        }
        private void UpdatePlayAgainButtonVisibility()
        {
            if (cardMechanics.isGameFinished == true)
            {
                PlayAgainButton.Visibility = Visibility.Visible;
                HitButton.IsEnabled = false;
                StandButton.IsEnabled = false;
                DoubleButton.IsEnabled = false;
                SplitButton.IsEnabled = false;
            }
            else
            {
                PlayAgainButton.Visibility = Visibility.Hidden;
            }
        }

        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            cardMechanics.UserHasSplit = true;
            CardTotalUserSplitLabel.Visibility = Visibility.Visible;

            Cards cardToSplit = cardMechanics.UserCards[1];

            cardMechanics.UserCards.Remove(cardToSplit);
            cardMechanics.UserCardsSplit.Add(cardToSplit);

            FirstCardImageUserSplit.Source = new BitmapImage(new Uri(cardToSplit.ImagePathFront, UriKind.Relative));

            Cards cardNew = cardMechanics.DealCardUser();
            SecondCardImageUser.Source = new BitmapImage(new Uri(cardNew.ImagePathFront, UriKind.Relative));

            if ((cardNew.ID == "Hearts Ace" || cardNew.ID == "Spades Ace" ||
                cardNew.ID == "Diamonds Ace" || cardNew.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
            {
                cardNew.Value = 1;
            }
            cardMechanics.CheckBlackJack();
            if (cardMechanics.CheckBlackJackIsTrue == true)
            {
                cardMechanics.UserCards.Clear();
                UserHasSplitAndStood = true;
            }
            CardTotalUserLabel.Content = cardMechanics.CalculateHandValueUser();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            Cards card2NewHand = cardMechanics.DealCardUserSplit();
            SecondCardImageUserSplit.Source = new BitmapImage(new Uri(card2NewHand.ImagePathFront, UriKind.Relative));

            if ((card2NewHand.ID == "Hearts Ace" || card2NewHand.ID == "Spades Ace" ||
                card2NewHand.ID == "Diamonds Ace" || card2NewHand.ID == "Clover Ace") && cardMechanics.CalculateHandValueUser() > 21)
            {
                card2NewHand.Value = 1;
            }
            cardMechanics.CheckBlackJackSplit();
            if (cardMechanics.CheckBlackJackSplitIsTrue == true)
            {
                cardMechanics.UserCardsSplit.Clear();
                UserHasSplitAndStood = true;
            }
            CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            SplitButton.IsEnabled = false;
        }
    }

}
