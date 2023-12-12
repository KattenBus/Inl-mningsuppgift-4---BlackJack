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
        //Råkade göra något här. Tror det såg ut såhär innan? Det verkar funka iaf.
        public string currentUser;
        public int score = 10;
        List<Player> PlayerList = new List<Player>();
        List<UserBalance> userBalanceList = new List<UserBalance>();
        //Player player1 = new Player(10, "hi");
        

        public GameMenu()
        {
            InitializeComponent();
            //PlayerList.Add(player1);
        }

        private void GoToHighScoreScreenButton_Click(object sender, RoutedEventArgs e)
        {                                  
            HighScoreScreen highScoreScreen= new HighScoreScreen();
            highScoreScreen.SetPlayerList(PlayerList);
            highScoreScreen.SetListBox();
            highScoreScreen.Show();               
        }
        private void GoToBlackJackButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewPlayer();
            //Startar ny version av kortleken.
            CardDeck newCardDeck = new CardDeck();
            CardMechanics newCardMechanics = new CardMechanics(newCardDeck);
            newCardMechanics.SetPlayerList(PlayerList, currentUser);
            BlackJackScreen blackJackScreen = new BlackJackScreen(newCardMechanics);

            //blackJackScreen.SetCardMechanic(newCardMechanics);
            blackJackScreen.Show();
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ManageAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ManageAccountScreen manageAccountScreen = new ManageAccountScreen();
            manageAccountScreen.SetUserBalanceListAndUser(userBalanceList, currentUser);
            manageAccountScreen.Show();
        }

        private void AddUserToBalanceList()
        {
            foreach (UserBalance userBalance in userBalanceList)
            {
                if (currentUser != userBalance.Username)
                {
                    UserBalance newUser = new UserBalance(currentUser, 0);
                    userBalanceList.Add(newUser);
                    return;
                }
            }
        }

        private void AddNewPlayer()
        {
            foreach (Player player in PlayerList)
            {
                if (currentUser == player.Name)
                {
                    return;
                }               
            }
            
            Player newPlayer = new Player(score, currentUser);
            PlayerList.Add(newPlayer);
        }
    }
}
