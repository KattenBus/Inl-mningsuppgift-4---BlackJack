using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        List<Accounts> accountList;

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
            changePasswordPanel.Visibility = Visibility.Hidden;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            gameMenu.DisplayBalance();
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
            changePasswordPanel.Visibility = Visibility.Hidden;
            finishedLabel.Content = "";
        }

        public void SetAllLists(List<UserBalance> userBalanceList, string currentUser, List<Accounts> accountList)
        {
            this.userBalanceList = userBalanceList;
            this.currentUser = currentUser;
            this.accountList = accountList;
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            changePasswordPanel.Visibility = Visibility.Hidden;
            withdrawAndDepositPanel.Visibility = Visibility.Visible;
            displayLabel.Content = "Withdraw";
            withdrawOrDepositButton.Content = "Withdraw";
            finishedLabel.Content = "";
        }

        private void depositButton_Click(object sender, RoutedEventArgs e)
        {
            changePasswordPanel.Visibility = Visibility.Hidden;
            withdrawAndDepositPanel.Visibility = Visibility.Visible;
            displayLabel.Content = "Deposit";
            withdrawOrDepositButton.Content = "Deposit";
            finishedLabel.Content = "";
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawAndDepositPanel.Visibility = Visibility.Hidden;
            changePasswordPanel.Visibility = Visibility.Visible;
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
                            StoreBalanceAccount();
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
                        StoreBalanceAccount();
                        finishedLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        finishedLabel.Content = $"You deposited {depositAmount}";
                    }
                }
            }

            amountInput.Text = "";
        }

        private void performChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPasswordInput = oldPassword.Password;
            string newPasswordInput = newPassword.Password;
            string confirmPasswordInput = confirmPassword.Password;

            if (newPasswordInput == confirmPasswordInput)
            {
                foreach (Accounts accounts in accountList)
                {
                    if (accounts.Password == oldPasswordInput && accounts.Username == currentUser)
                    {
                        accounts.Password = newPasswordInput;
                        StoreUpdatedAccount();
                        finishedLabel2.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        finishedLabel2.Content = "You have sucessfully changed your password";    
                        ResetPasswordBox();
                        return;
                    }
                }
                finishedLabel2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                finishedLabel2.Content = "Your current password is incorrect";
            }

            else if (newPasswordInput == "" && confirmPasswordInput == "")
            {
                finishedLabel2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                finishedLabel2.Content = "You did not enter your new password or confirm your new password";
            }

            else
            {
                finishedLabel2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                finishedLabel2.Content = "The passwords did not match";
            }

            ResetPasswordBox();
        }

        private void ResetPasswordBox()
        {
            oldPassword.Password = "";
            newPassword.Password = "";
            confirmPassword.Password = "";
        }

        string folderPath = "csvFolder";
        string path = "csvFolder/accounts.csv";
        string absolutePath = "C:\\Users\\minht\\source\\repos\\Gruppuppgift4\\Gruppuppgift4";
        private void StoreUpdatedAccount()
        {
            Directory.CreateDirectory(folderPath);
            File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Username,Password");
                foreach (Accounts accounts in accountList)
                {
                    sw.WriteLine(accounts.GetCSV());
                }
            }
        }

        string path2 = "csvFolder/balanceAccounts.csv";
        public void StoreBalanceAccount()
        {
            Directory.CreateDirectory(folderPath);
            File.Delete(path2);

            using (StreamWriter sw = new StreamWriter(path2))
            {
                sw.WriteLine("Username,Balance");
                foreach (UserBalance userBalance in userBalanceList)
                {
                    sw.WriteLine(userBalance.GetCSV());
                }
            }
        }
    }
}
