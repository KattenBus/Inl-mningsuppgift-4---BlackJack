using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GruppInlämning_4___BlackJack
{
    public class CardMechanics
    {   //Listan med korten.
        public List<Cards> CardList { get; set; }
        //Skapar nya listor för delaer och User som sparar korten de tilldelas i sig.
        public List<Cards> UserCards = new List<Cards>();
        public List<Cards> DealerCards = new List<Cards>();
        Random randomCard = new Random();
        public bool isGameFinished = false;

        //Här sparas vinsterna, den ska med till HIGHSCORESCREEN!
        public int totalScore = 0;

        public CardMechanics(CardDeck cardDeck)
        {
            CardList = cardDeck.CardList;
        }
        //Funktion som väljer ett slumpat kort från CardList och flyttar kortet till UserCards listan.
        public Cards DealCardUser()
        {
            //if (UserCards.Count == 0)
            //{
            //    int chooseRandomCard = randomCard.Next(0, CardList.Count);
            //    Cards dealtCardUser = CardList[chooseRandomCard];

            //    CardList.RemoveAt(chooseRandomCard);
            //    UserCards.Add(dealtCardUser);

            //    chooseRandomCard = randomCard.Next(0, CardList.Count);
            //    dealtCardUser = CardList[chooseRandomCard];

            //    CardList.RemoveAt(chooseRandomCard);
            //    UserCards.Add(dealtCardUser);

            //    return dealtCardUser;
            //}
            //else 
            //{
                int chooseRandomCard = randomCard.Next(0, CardList.Count);
                Cards dealtCardUser = CardList[chooseRandomCard];

                CardList.RemoveAt(chooseRandomCard);
                UserCards.Add(dealtCardUser);

                return dealtCardUser;
            //}
        }
        //Funktion som väljer ett slumpat kort från CardList och flyttar kortet till DealerCards listan.
        public Cards DealCardDealer()
        {
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
        public Cards Hit()
        { 
             int chooseRandomCard = randomCard.Next(0, CardList.Count);
             Cards drawnCard = CardList[chooseRandomCard];

             CardList.RemoveAt(chooseRandomCard);
             UserCards.Add(drawnCard);

             return drawnCard;      
        }
        //Man är nöjd med sina kort och lämnar över spelet till dealern
        public void Stand()
        {
            DealersTurn();
        }
        //Splitta korten och spela på två händer samtidigt.
        public void Split()
        {
            //if (UserCards.Count == 2 && UserCards[0].Value == UserCards[1].Value)
            //{ 
            
            //}
        }
        //Kollar om User har fått Blackjack(Två kort som tillsammans blir 21)
        public void CheckBlackJack()
        {
            if (CalculateHandValueUser() == 21 && UserCards.Count == 2)
            {
                totalScore += 2;
                RoundEnd();
                MessageBox.Show("Hurray! You got BlackJack");           
            }
        }
        public void CheckBlackJackDealer()
        {
            if (CalculateHandValueDealer() == 21 && DealerCards.Count == 2)
            {
                //PointsEarned();
                RoundEnd();
                MessageBox.Show("The dealer got BackJack. YOU LOOSE!");
            }
        }
        //Kollar om User har kort på handen som överstiger ett värde av 21.
        public void CheckBust()
        {
            if (CalculateHandValueUser() >= 22)
            {
                totalScore -= 1;
                RoundEnd();
                MessageBox.Show("You got more than 21, Bust! YOU LOOSE");                
            }    
        }
        /*public bool CheckBustDealer()
        {
            if (CalculateHandValueUser() >= 22)
            {
                RoundEnd();
                MessageBox.Show("The dealer busted! YOU WIN!");
            }
            return true;
        }*/

        //Kod som ska hoppa igång efter User är klar med sin runda.
        public void DealersTurn()
        {
            while (CalculateHandValueDealer() <= 16)
            {        
                    //DealCardDealer();
            }
            if (CalculateHandValueDealer() >= 22)
            {
                totalScore += 1;
                MessageBox.Show("The dealer busted! YOU WIN!");
            }
            else if (CalculateHandValueDealer() == CalculateHandValueUser())
            {
                MessageBox.Show("Woah! Even Steven! Let's go again!");
            }
            else if (CalculateHandValueDealer() < CalculateHandValueUser())
            {
                totalScore += 1;
                MessageBox.Show("You Managed to WIN!");              
            }
            else if (CalculateHandValueDealer() > CalculateHandValueUser())
            {
                totalScore -= 1;
                MessageBox.Show("The dealer won by a smidge!");
            }
            //PointsEarned();
            RoundEnd();
        }
        //public int PointsEarned()
        //{
        //    //if (CalculateHandValueUser() == 21 && UserCards.Count == 2)
        //    //{
        //    //    totalScore += 2;
        //    //}
        //    else if (CalculateHandValueDealer() >= 22)
        //    {
                
        //    }
        //    else if (CalculateHandValueUser() > CalculateHandValueDealer())
        //    {
                
        //    }
        //    else if (CalculateHandValueUser() < CalculateHandValueDealer())
        //    {
        //        totalScore -=1;
        //    }
        //    //else if (CalculateHandValueUser() >= 22)
        //    //{
        //    //    totalScore -=1;
        //    //}
        //    return totalScore;
        //}

        //Räknar ut vem av User eller Dealer som vann.
        public void RoundEnd()
        {
            isGameFinished = true;
        }
        //Kod som ska hoppa igång när programmet startar eller man startar en ny runda.
        public void NewRound()
        {
            ResetCardDeck();
        }
        private void ResetCardDeck()
        {
            UserCards.Clear();
            DealerCards.Clear();
            CardList = new CardDeck().CardList;
        }
    }
}
