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
        CardDeck cardDeck;

        public List<Cards> CardList { get; set; }
        public List<Cards> UserCards { get; set; }
        public List <Cards> DealerCards { get; set; }
        public CardMechanics CardMechanics { get; set; }
        
        public BlackJackScreen(CardDeck cardDeck, CardMechanics cardMechanics)
        {
            InitializeComponent();
            CardList = cardDeck.CardList;
            UserCards = cardMechanics.UserCards;
            DealerCards = cardMechanics.DealerCards;
        }

        public void SetCardMechanic(CardMechanics cardmechanics)
        {
            this.cardMechanics = cardmechanics;
        }

        public void UserCardsds()
        {

        }
        public void DealerCardsdsdsa()
        {

        }
        private void DealCardButton_Click(object sender, RoutedEventArgs e)
        {
            Cards card =  cardMechanics.DealCardUser();

            //Är menad att hämta bilden för det slumpade kortet.      
            FirstCardImageUser.Source = new BitmapImage(new Uri(card.ImagePathFront, UriKind.Relative));
            // Är menad att visa värdet på det slumpade kortet.
            CardTotalUserLabel.Content = card.Value.ToString();
            CardTotalUserLabel.Content = card.ID.ToString();
            CardIDUserLabel.Content = card.ID.ToString();



        }
    }
}
