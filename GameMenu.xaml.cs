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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Printing;

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Window
    {
        ManageAccountScreen manageAccountScreen = new ManageAccountScreen();
        //Råkade göra något här. Tror det såg ut såhär innan? Det verkar funka iaf.
        public string currentUser;
        List<Player> highscoreList = new List<Player>();
        List<UserBalance> userBalanceList = new List<UserBalance>();
        List<Accounts> accountList;
        
        public GameMenu()
        {
            InitializeComponent();
            LoadBalanceAccounts();
            LoadHighscoreList();
            manageAccountScreen.GameMenu = this;
        }

        public void SetAccountList(List<Accounts> accountList)
        {
            this.accountList = accountList;
        }

        private void GoToHighScoreScreenButton_Click(object sender, RoutedEventArgs e)
        {                                  
            HighScoreScreen highScoreScreen= new HighScoreScreen();
            highScoreScreen.SetAllLists(highscoreList, userBalanceList);
            highScoreScreen.SetListBox();
            highScoreScreen.Show();               
        }

        private void GoToBlackJackButton_Click(object sender, RoutedEventArgs e)
        {
            bool userHasPlayedBefore = AddNewPlayer();
            //Startar ny version av kortleken.
            CardDeck newCardDeck = new CardDeck();
            CardMechanics newCardMechanics = new CardMechanics(newCardDeck);
            newCardMechanics.SetAllLists(highscoreList, currentUser, userBalanceList);
            BlackJackScreen blackJackScreen = new BlackJackScreen(newCardMechanics);           
            blackJackScreen.GameMenu = this;
            blackJackScreen.SetAllLists(userBalanceList, currentUser, highscoreList);
            if (userHasPlayedBefore == false)
            {
                blackJackScreen.StoreHighscoreList();
            }

            //blackJackScreen.SetCardMechanic(newCardMechanics);
            blackJackScreen.Show();
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ManageAccountButton_Click(object sender, RoutedEventArgs e)
        {
            bool userHasBalanceAccount = AddUserToBalanceList();
            manageAccountScreen.SetAllLists(userBalanceList, currentUser, accountList);
            if (userHasBalanceAccount == false)
            {
                manageAccountScreen.StoreBalanceAccount();
            }
            manageAccountScreen.Show();
        }

        private bool AddUserToBalanceList()
        {
            foreach (UserBalance userBalance in userBalanceList)
            {
                if (currentUser == userBalance.Username)
                {                  
                    return true;
                }             
            }

            UserBalance newUser = new UserBalance(currentUser, 0);
            userBalanceList.Add(newUser);
            return false;
        }

        private bool AddNewPlayer()
        {
            foreach (Player player in highscoreList)
            {
                if (currentUser == player.Name)
                {
                    return true;
                }               
            }
            
            Player newPlayer = new Player(0, currentUser);
            highscoreList.Add(newPlayer);
            return false;
        }

        public void DisplayBalance()
        {
            foreach (UserBalance user in userBalanceList)
            {
                if (currentUser == user.Username)
                {
                    balanceLabel.Content = "Balance: " + user.GetBalance();
                }
            }
        }

        string folderPath = "csvFolder";
        string path = "csvFolder/balanceAccounts.csv";
        string absolutePath = "C:\\Users\\minht\\source\\repos\\Gruppuppgift4\\Gruppuppgift4";
        private void LoadBalanceAccounts()
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] strings = line.Split(",");

                        string username = strings[0];
                        int balance = int.Parse(strings[1]);

                        UserBalance userBalance = new UserBalance(username, balance);
                        userBalanceList.Add(userBalance);

                        line = sr.ReadLine();
                    }
                }
            }
        }

        string path2 = "csvFolder/highscoreList.csv";
        private void LoadHighscoreList()
        {
            FileInfo file = new FileInfo(path2);
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(path2))
                {
                    sr.ReadLine();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] strings = line.Split(",");

                        string name = strings[0];
                        int highScore = int.Parse(strings[1]);

                        Player player = new Player(highScore, name);
                        highscoreList.Add(player);

                        line = sr.ReadLine();
                    }
                }
            }
        }
    }
}
