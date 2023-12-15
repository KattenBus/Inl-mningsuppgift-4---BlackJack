using System;
using System.Collections.Generic;
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
    /// Interaction logic for RegistrationScreen.xaml
    /// </summary>
    public partial class RegistrationScreen : Window
    {
        List<Accounts> accountList;
        public RegistrationScreen()
        {
            InitializeComponent();
        }

        public void SetAccountList(List<Accounts> accountList)
        {
            this.accountList = accountList;
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            string username = newUsernameInput.Text;
            string password = newPasswordInput.Password;
            string confirmPassword = confirmNewPasswordInput.Password;
            bool existingAccount = false;

            if (agreeTermsAndConditon.IsChecked == false)
            {
                failedRegistrationLabel.Foreground = new SolidColorBrush(Color.FromArgb(255,255,0,0));
                failedRegistrationLabel.Content = "You need to agree to our terms and conditions";
                return;
            }

            foreach (Accounts accounts in accountList)
            {
                if (username == accounts.Username)
                {
                    failedRegistrationLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    failedRegistrationLabel.Content = "An account with this username already exists";
                    newPasswordInput.Password = "";
                    confirmNewPasswordInput.Password = "";
                    existingAccount = true;
                    return;
                }
            }

            if (existingAccount == false)
            {
                if (password.Equals(confirmPassword) && password != "" && confirmPassword != "")
                {
                    Accounts newAcc = new Accounts(username, password);
                    accountList.Add(newAcc);
                    failedRegistrationLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    failedRegistrationLabel.Content = "You have sucessfully registered an account";
                    StoreAccount();
                }

                else if (password == "" && confirmPassword == "")
                {
                    failedRegistrationLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    failedRegistrationLabel.Content = "You did not enter a password or confirm your password";
                }

                else
                {
                    failedRegistrationLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    failedRegistrationLabel.Content = "The passwords did not match";
                }
            }
            newPasswordInput.Password = "";
            confirmNewPasswordInput.Password = "";
        }

        string folderPath = "csvFolder";
        string path = "csvFolder/accounts.csv";
        string absolutePath = "C:\\Users\\minht\\source\\repos\\Gruppuppgift4\\Gruppuppgift4";
        
        private void StoreAccount()
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

        private void returnToLogIn_Click(object sender, RoutedEventArgs e)
        {
            Hide();           
        }
    }
}
