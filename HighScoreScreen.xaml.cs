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
        List<Player> highscoreList;
        List<UserBalance> userBalanceList;

        //Highscore är wins in a row.
        //userBalanceList är med här för vi skulle ha med balance på något 
        //användbartsätt i HighscoreScreen men det blev ej av.

        public HighScoreScreen()
        {
            InitializeComponent();
        }
        
        public void SetAllLists(List<Player> highscoreList, List<UserBalance> userBalanceList)
        {
            this.highscoreList = highscoreList;
            this.userBalanceList = userBalanceList;
        }

        public void SetListBox()
        {
            HighScoreListBox.Items.Clear();
            foreach (Player player in highscoreList)
            {              
                HighScoreListBox.Items.Add($"Name: {player.Name} | Score: {player.HighScore}");
            }
        }
    }
}
