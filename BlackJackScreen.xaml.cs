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
    /// Interaction logic for BlackJackScreen.xaml
    /// </summary>
    public partial class BlackJackScreen : Window
    {
        CardMechanics cardMechanics;     
        public BlackJackScreen(CardMechanics cardMechanics)
        {
            InitializeComponent();
            SetCardMechanic(cardMechanics);

        }

        public void SetCardMechanic(CardMechanics cardmechanics)
        {
            this.cardMechanics = cardmechanics;
        }

        public void UserHand()
        {
            Cards card = cardMechanics.DealCardUser();

            FirstCardImageUser.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            CardIDUserLabel.Content = card.Value.ToString();
            CardIDUserLabel.Content = card.ID.ToString();
        }
        public void DealerHand()
        {
            Cards card = cardMechanics.DealCardDealer();

            FirstCardImageDealer.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            CardIDDealerLabel.Content = card.Value.ToString();
            CardIDDealerLabel.Content = card.ID.ToString();
        }
        private void DealCardButton_Click(object sender, RoutedEventArgs e)
        {
            UserHand();
            DealerHand();   
        }
    }
}
