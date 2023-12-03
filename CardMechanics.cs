using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace GruppInlämning_4___BlackJack
{
    public class CardMechanics
    {   //Listan med korten.
        public List<Cards> CardList { get; set; }
        //Skapar nya listor för delaer och User som sparar korten de tilldelas i sig.
        public List<Cards> UserCards= new List<Cards>();
        public List<Cards> DealerCards = new List<Cards>();

        public CardMechanics(CardDeck cardDeck)
        {
            CardList = cardDeck.CardList;
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
        //Räknar ut värdet på korten i UserList.
        public int CalculateHandValueUser()
        {
            int totalValueUser = 0;

            foreach (var card in UserCards)
            {
                totalValueUser += card.Value;
            }
            return totalValueUser;
        }
        //Räknar ut värdet på korten i DealerList.
        public int CalculateHandValueDealer()
        {
            int totalValueDealer = 0;

            foreach (var card in DealerCards)
            {
                totalValueDealer += card.Value;
            }
            return totalValueDealer;
        }
        //Ta ett till kort.
        public void Hit()
        {
            Random randomCard = new Random();
            int chooseRandomCard = randomCard.Next(0, CardList.Count);

            if (CalculateHandValueUser() <= 21)
            {
                CardList.RemoveAt(chooseRandomCard);
                UserCards.Add(CardList[chooseRandomCard]);
            }
            else 
            {
                MessageBox.Show("You cant draw any more cards!");
            }
        }
        //Man är nöjd med sina kort och lämnar över spelet till dealern
        public void Stand()
        {
            //DealerTurnfunktionen ska starta.
        }
        //Splitta korten och spela på två händer samtidigt.
        public void Split()
        {
            //Inte riktigt säker på hur jag ska lösa den.
        }
        //Kollar om User har fått Blackjack(Två kort som tillsammans blir 21)
        public void CheckBlackJack()
        {
            Random randomCard = new Random();
            int chooseRandomCard = randomCard.Next(0, CardList.Count);

            if (CalculateHandValueUser() == 21 && UserCards.Count == 2)
            {
                MessageBox.Show("Hurray! You got BlackJack");
            }
        }
        //Kollar om User har kort på handen som överstiger ett värde av 21.
        public void CheckBust()
        {
            Random randomCard = new Random();
            int chooseRandomCard = randomCard.Next(0, CardList.Count);

            if (CalculateHandValueUser() >= 21)
            {
                MessageBox.Show("You got more than 21, Bust!");
            }
        }
        //Kod som ska hoppa igång efter User är klar med sin runda.
        public void DealersTurn()
        {
            //Kanske.
        }
        //Räknar ut vem av User eller Dealer som vann.
        public void RoundEnd()
        {
            int UserTotalValue = CalculateHandValueUser();
            int DealerTotalValue = CalculateHandValueDealer();

            if (UserTotalValue > DealerTotalValue)
            {
                MessageBox.Show("You Won!");
            }
            else if (UserTotalValue == DealerTotalValue)
            {
                MessageBox.Show("Draw! Play again");
            }
            else if (UserTotalValue <= DealerTotalValue)
            {
                MessageBox.Show("You Loose!");
            }
        }
        //Kod som ska hoppa igång när programmet startar eller man startar en ny runda.
        public void NewRound() 
        { 
        
        }
    }
}
