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

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for ManageAccountScreen.xaml
    /// </summary>
    public partial class ManageAccountScreen : Window
    {
        List<UserBalance> userBalanceList;
        string currentUser;
        public ManageAccountScreen()
        {
            InitializeComponent();
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
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
        }

        private void depositButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawAndDepositPanel.Visibility = Visibility.Visible;
            displayLabel.Content = "Deposit";
            withdrawOrDepositButton.Content = "Deposit";
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {

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
                            MessageBox.Show($"You withdrew {withdrawAmount}");
                        }

                        else
                        {
                            MessageBox.Show("You are dead broke");
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
                    }
                }
            }
        }
    }
}
