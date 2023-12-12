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
    /// Interaction logic for HighScoreScreen.xaml
    /// </summary>
    public partial class HighScoreScreen : Window
    {
        List<Player> playerList;
        List<UserBalance> userBalanceList;


        public HighScoreScreen()
        {
            InitializeComponent();
            //PopulateHighScoreList();
            
        }
        
        public void SetAllLists(List<Player> playerList, List<UserBalance> userBalanceList)
        {
            this.playerList = playerList;
            this.userBalanceList = userBalanceList;
        }

        public void SetListBox()
        {
            HighScoreListBox.Items.Clear();
            foreach (Player player in playerList)
            {              
                HighScoreListBox.Items.Add(player.Name + "-" + player.HighScore);
            }
        }
        //private void PopulateHighScoreList()
        //{
        //    PlayerList.Add(new Player(cardMechanics.totalScore, Name));
        //    foreach (Player player in PlayerList)
        //    {
        //        HighScoreListBox.Items.Add($"{player.Name} - {player.HighScore}");
        //    }
        //}
    }
}
