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
    /// Interaction logic for LogInScreen.xaml
    /// </summary>
    public partial class LogInScreen : Window
    {
        List<Accounts> accountList = new List<Accounts>();

        Accounts testAccount = new Accounts("1", "1");
        public LogInScreen()
        {
            InitializeComponent();
            accountList.Add(testAccount);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameInput.Text;
            string password = passwordInput.Password;

            foreach (Accounts accounts in accountList)
            {
                if (username == accounts.Username && password == accounts.Password) 
                {
                    passwordInput.Password = "";
                    GameMenu gameMenu = new GameMenu();
                    gameMenu.welcomeLabel.Content = $"Welcome {username}!";
                    gameMenu.currentUser = username;
                    gameMenu.Show();                 
                    return;
                }
            }

            passwordInput.Password = "";
            failLabel.Content = "Your username or password was incorrect";
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationScreen registrationScreen = new RegistrationScreen();
            registrationScreen.Show();
            registrationScreen.SetAccountList(accountList);
        }
    }
}
