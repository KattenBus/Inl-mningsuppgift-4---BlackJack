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

namespace GruppInlämning_4___BlackJack
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Window
    {
        public GameMenu()
        {
            InitializeComponent();
        }

        private void GoToHighScoreScreenButton_Click(object sender, RoutedEventArgs e)
        {
            HighScoreScreen highScoreScreen= new HighScoreScreen();
            highScoreScreen.Show();
        }

        private void GoToBlackJackButton_Click(object sender, RoutedEventArgs e)
        {
            BlackJackScreen blackJackScreen= new BlackJackScreen();
            blackJackScreen.Show();
        }
    }
}
