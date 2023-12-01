using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GruppInlämning_4___BlackJack
{
    public class CardMechanics
    {   //Listan med korten.
        public List<Cards> CardList;
        //Skapar nya listor för delaer och User som sparar korten de tilldelas i sig.
        public List<Cards> UserCards= new List<Cards>();
        public List<Cards> DealerCards = new List<Cards>();

        public CardMechanics(List<Cards> cardList)
        {
            CardList = cardList;
        }

        //Funktion som väljer ett slumpat kort från CardList och flyttar kortet till UserCards listan.
        public Cards DealCardUser()
        {
            Random randomCard = new Random();
            int chooseRandomCard = randomCard.Next(0, CardList.Count);

            Cards dealtCardUser = CardList[chooseRandomCard];

            CardList.RemoveAt(chooseRandomCard);
            UserCards.Add(dealtCardUser);

            return dealtCardUser;
        }
        //Funktion som väljer ett slumpat kort från CardList och flyttar kortet till DealerCards listan.
        public Cards DealCardDealer()
        {
            Random randomCard = new Random();
            int chooseRandomCard = randomCard.Next(0, CardList.Count);

            Cards dealtCardDealer = CardList[chooseRandomCard];

            CardList.RemoveAt(chooseRandomCard);
            DealerCards.Add(dealtCardDealer);

            return dealtCardDealer;
        }


    }
}
