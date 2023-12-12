using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for ManageAccountScreen.xaml
    /// </summary>
    public partial class ManageAccountScreen : Window
    {
        List<UserBalance> userBalanceList;
        string currentUser;

        private GameMenu gameMenu;
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
        public ManageAccountScreen()
        {
            InitializeComponent();
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            gameMenu.DisplayBalance();
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
            finishedLabel.Content = "";
        }

        public void SetUserBalanceListAndUser(List<UserBalance> userBalanceList, string currentUser)
        {
            this.userBalanceList = userBalanceList;
            this.currentUser = currentUser;
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawAndDepositPanel.Visibility = Visibility.Visible;
            displayLabel.Content = "Withdraw";
            withdrawOrDepositButton.Content = "Withdraw";
            finishedLabel.Content = "";
        }

        private void depositButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawAndDepositPanel.Visibility = Visibility.Visible;
            displayLabel.Content = "Deposit";
            withdrawOrDepositButton.Content = "Deposit";
            finishedLabel.Content = "";
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
        }

        private void withdrawOrDepositButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayLabel.Content.ToString() == "Withdraw")
            {
                foreach (UserBalance userBalance in userBalanceList)
                {
                    if (currentUser == userBalance.Username)
                    {
                        int withdrawAmount = int.Parse(amountInput.Text);
                        bool hasWithdrawn = userBalance.RemoveBalance(withdrawAmount);

                        if (hasWithdrawn == true)
                        {
                            finishedLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                            finishedLabel.Content = $"You have withdrawn {withdrawAmount}";
                        }

                        else
                        {
                            finishedLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                            finishedLabel.Content = "You dont have enough for this";
                        }
                    }
                }
            }

            if (displayLabel.Content.ToString() == "Deposit")
            {
                foreach (UserBalance userBalance in userBalanceList)
                {
                    if (currentUser == userBalance.Username)
                    {
                        int depositAmount = int.Parse(amountInput.Text);
                        userBalance.AddBalance(depositAmount);
                        finishedLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        finishedLabel.Content = $"You deposited {depositAmount}";
                    }
                }
            }

            amountInput.Text = "";
        }
    }
}
