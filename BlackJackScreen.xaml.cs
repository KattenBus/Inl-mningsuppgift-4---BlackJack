﻿using System;
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
using System.ComponentModel;
using System.Reflection;
using System.Media;
using System.IO;
using Path = System.IO.Path;

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for BlackJackScreen.xaml
    /// </summary>
    public partial class BlackJackScreen : Window
    {
        CardMechanics cardMechanics;
        public bool UserHasSplitAndStood = false;
        List<UserBalance> userBalanceList;
        List<Player> highscoreList;
        private GameMenu gameMenu;
        string currentUser;
        int totalBet;
        private SoundPlayer player;
             
        public GameMenu GameMenu
        {
            set
            {
                if (gameMenu != value)
                {
                    gameMenu = value;
                }
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            PerformPlayAgainButtonLogic();
            StoreBalanceAccount();
            StoreHighscoreList();
            gameMenu.DisplayBalance();          
        }
        public BlackJackScreen(CardMechanics cardMechanics)
        {
            InitializeComponent();
            SetCardMechanic(cardMechanics);
            string audioFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio");
            string soundFilePath = Path.Combine(audioFolderPath, "flipcard.wav");
            player = new SoundPlayer(soundFilePath);
        }
        public void SetAllLists(List<UserBalance> userBalanceList, string currentUser, List<Player> highscoreList)
        {
            this.userBalanceList = userBalanceList;
            this.currentUser = currentUser;
            this.highscoreList = highscoreList;
        }
        public void SetCardMechanic(CardMechanics cardmechanics)
        {
            this.cardMechanics = cardmechanics;
        }
        public void DealHandUser()
        {
            Cards card = cardMechanics.DealCardUser();
            FirstCardImageUser.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            player.Play();
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
            if (totalBet == 0)
            {
                MessageBox.Show("Please place a bet.");
                return;
            }
            bool doesUserHaveBalanceAccount = DoesBalanceAccountExist();
            if (doesUserHaveBalanceAccount == false)
            {
                MessageBox.Show("You need to start a balanceaccount by depositing!");
                return;
            }

            bool betIsMade = BetOnClickAction();
            if (betIsMade == false)
            {
                return;
            }

            Bet100.IsEnabled = false;
            Bet200.IsEnabled = false;
            Bet500.IsEnabled = false;
            ResetBet.IsEnabled = false;

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
            bool betIsMade = BetOnClickAction();
            if (betIsMade == false)
            {
                return;
            }

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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
        }
        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    SixthCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
                if (cardMechanics.UserHasSplit == true && cardMechanics.CalculateHandValueUser() >= 22 && cardMechanics.CheckBlackJackSplitIsTrue == true)
                {
                    FirstCardImageUser.Source = null;
                    SecondCardImageUser.Source = null;
                    ThirdCardImageUser.Source = null;
                    FourthCardImageUser.Source = null;
                    FifthCardImageUser.Source = null;
                    SixthCardImageUser.Source = null;
                    SeventhCardImageUser.Source = null;
                    cardMechanics.UserCards.Clear();
                    PerformStandButton_ClickLogic();
                }
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
            else if (cardMechanics.CheckBlackJackSplitIsTrue == true)
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
            player.Play();
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
            PerformPlayAgainButtonLogic();
            CurrentBalance();
        }

        private void PerformPlayAgainButtonLogic()
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
            Bet100.IsEnabled = true;
            Bet200.IsEnabled = true;
            Bet500.IsEnabled = true;
            ResetBet.IsEnabled = true;
            UpdatePlayAgainButtonVisibility();
        }
        private void UpdatePlayAgainButtonVisibility()
        {
            if (cardMechanics.isGameFinished == true)
            {
                PlayAgainButton.Visibility = Visibility.Visible;
                totalBet = 0;
                totalBetLabel.Content = "Total bet:";
                
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
            player.Play();
            bool betIsMade = BetOnClickAction();
            if (betIsMade == false)
            {
                return;
            }

            cardMechanics.UserHasSplit = true;
            CardTotalUserSplitLabel.Visibility = Visibility.Visible;

            Cards cardToSplit = cardMechanics.UserCards[0];

            cardMechanics.UserCards.Remove(cardToSplit);
            cardMechanics.UserCardsSplit.Add(cardToSplit);

            if (cardToSplit.ID.Contains("Ace"))
            { 

            cardToSplit.Value = 11;

            }

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
            }
            CardTotalUserSplitLabel.Content = cardMechanics.CalculateHandValueUserSplit();
            totalWinsLabel.Content = "Total Wins: " + cardMechanics.totalScore;

            SplitButton.IsEnabled = false;
        }

        private bool BetOnClickAction()
        {
            foreach (UserBalance userBalance in userBalanceList)
            {
                if (currentUser == userBalance.Username)
                {
                    bool betIsMade = userBalance.RemoveBalance(totalBet);
                    
                    if (betIsMade == true)
                    {
                        cardMechanics.SetTotalBetAmount(totalBet);
                        return true;
                    }

                    if (betIsMade == false)
                    {
                        MessageBox.Show("You don't have enough to bet");
                        return false;
                    }
                }
            }
            return false;
        }
        private bool DoesBalanceAccountExist()
        {
            foreach (UserBalance userBalance in userBalanceList)
            {
                if (currentUser == userBalance.Username)
                {
                    return true;
                }
            }

            return false;
        }

        string folderPath = "csvFolder";
        string path = "csvFolder/balanceAccounts.csv";
        
        public void StoreBalanceAccount()
        {
            Directory.CreateDirectory(folderPath);
            File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Username,Balance");
                foreach (UserBalance userBalance in userBalanceList)
                {
                    sw.WriteLine(userBalance.GetCSV());
                }
            }
        }

        string path2 = "csvFolder/highscoreList.csv";
        public void StoreHighscoreList()
        {
            Directory.CreateDirectory(folderPath);
            File.Delete(path2);

            using (StreamWriter sw = new StreamWriter(path2))
            {
                sw.WriteLine("Username,Highscore");
                foreach (Player player in highscoreList)
                {
                    sw.WriteLine(player.GetCSV());
                }
            }
        }

        private void ResetBet_Click(object sender, RoutedEventArgs e)
        {
            totalBet = 0;
            totalBetLabel.Content = $"Total bet: {totalBet}";
        }

        private void Bet100_Click(object sender, RoutedEventArgs e)
        {
            totalBet += 100;
            totalBetLabel.Content = $"Total bet: {totalBet}";
            
        }

        private void Bet200_Click(object sender, RoutedEventArgs e)
        {
            totalBet += 200;
            totalBetLabel.Content = $"Total bet: {totalBet}";
        }

        private void Bet500_Click(object sender, RoutedEventArgs e)
        {
            totalBet += 500;
            totalBetLabel.Content = $"Total bet: {totalBet}";
        }

        public void CurrentBalance()
        {
            foreach (UserBalance user in userBalanceList)
            {
                if (currentUser == user.Username)
                {
                    int userBalance = user.GetBalance();                                        
                    balanceLabel.Content = $"Balance: {userBalance}";
                    return;
                }
            }
                        
        }
    }
}
