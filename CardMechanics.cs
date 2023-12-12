using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Security.RightsManagement;

namespace GruppInlämning_4___BlackJack
{
    public class CardMechanics
    {   //Listan med korten.
        public List<Cards> CardList { get; set; }
        //Skapar nya listor för delaer och User som sparar korten de tilldelas i sig.
        public List<Cards> UserCards = new List<Cards>();
        public List<Cards> DealerCards = new List<Cards>();
        public List<Cards> UserCardsSplit = new List<Cards>();
        public List<Player> PlayerList;
        Random randomCard = new Random();
        public bool isGameFinished = false;
        public bool DoubleInitiated = false;
        public bool DoubleInitiatedSplit = false;
        public bool CanSplit = false;
        public bool UserHasSplit = false;
        public bool CheckBlackJackSplitIsTrue = false;
        public bool CheckBlackJackIsTrue = false;
        string currentUser;

        //Här sparas vinsterna, den ska med till HIGHSCORESCREEN!
        public int totalScore {  get; set; }
               
        public CardMechanics(CardDeck cardDeck)
        {
            CardList = cardDeck.CardList;
            
        }
        //Funktion som väljer ett slumpat kort från CardList och flyttar kortet till UserCards listan.

        public void SetPlayerList(List<Player> PlayerList, string currentUser)
        {
            this.PlayerList = PlayerList;
            this.currentUser = currentUser;
        }
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
        public Cards DealCardUserSplit()
        {
            int chooseRandomCard = randomCard.Next(0, CardList.Count);
            Cards dealtCardUser = CardList[chooseRandomCard];

            CardList.RemoveAt(chooseRandomCard);
            UserCardsSplit.Add(dealtCardUser);

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
        public int CalculateHandValueUserSplit()
        {
            int totalValueUser = 0;

            foreach (var card in UserCardsSplit)
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

        //Splitta korten och spela på två händer samtidigt.9
        public bool Split()
        {
            if (UserCards.Count == 2 && (UserCards[0].Value == UserCards[1].Value || 
                UserCards.Count == 2 && UserCards[0].ID.Contains("Ace") && UserCards[1].ID.Contains("Ace")))               
            {
                CanSplit = true;
            }
            return CanSplit;
        }
        public bool UserDidSplit()
        {
            UserHasSplit = true;

            return UserHasSplit;
        }
        //Kollar om User har fått Blackjack(Två kort som tillsammans blir 21)
        public void CheckBlackJack()
        {
            if (CalculateHandValueUser() == 21 && UserCards.Count == 2 && UserHasSplit == true)
            {
                if (CalculateHandValueUser() == 21 && UserCards.Count == 2)
                {
                    MessageBox.Show("Hurray! You got BlackJack");
                    totalScore += 2;
                    CheckBlackJackIsTrue = true;
                }

            }
            else if (CalculateHandValueUser() == 21 && UserCards.Count == 2 && UserHasSplit == false)
            {
                totalScore += 2;
                CheckBlackJackIsTrue = true;
                RoundEnd();
                MessageBox.Show("Hurray! You got BlackJack");
            }

        }
        public void CheckBlackJackSplit()
        {
            if (CalculateHandValueUserSplit() == 21 && UserCardsSplit.Count == 2 && UserHasSplit == true)
            {
                if (CalculateHandValueUserSplit() == 21 && UserCards.Count == 2)
                {
                    MessageBox.Show("Hurray! You got BlackJack");
                    totalScore += 2;
                    CheckBlackJackSplitIsTrue = true; 
                }
            }
        }
        public void CheckBlackJackDealer()
        {
            if (CalculateHandValueDealer() == 21 && DealerCards.Count == 2)
            {
                //PointsEarned();
                RoundEnd();
                MessageBox.Show("The dealer got BackJack.");
            }
        }
        //Kollar om User har kort på handen som överstiger ett värde av 21.
        public void CheckBust()
        {
            if (UserHasSplit == true && CalculateHandValueUser() >= 22 )
            {
                if (DoubleInitiated == true)
                {
                    totalScore -= 2;
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE YOUR FIRST HAND!!");
                }
                else
                {
                    totalScore -= 1;
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE YOUR FIRST HAND!!");
                }
            }
            else if (UserHasSplit == false && CalculateHandValueUser() >= 22)
            {
                if (DoubleInitiated == true)
                {
                    totalScore -= 2;
                    RoundEnd();
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE.");
                }
                else
                {
                    totalScore -= 1;
                    RoundEnd();
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE.");
                }
            }
        }
        public void CheckBustSplit()
        {
            if (CalculateHandValueUserSplit() >= 22)
            {
                if (DoubleInitiated == true)
                {
                    totalScore -= 2;
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE YOUR SECOND HAND!");
                }
                else
                {
                    totalScore -= 1;
                    MessageBox.Show("You got more than 21, Bust! YOU LOOSE YOU SECOND HAND!");
                }
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

        //Man får bara dra ett kort till, men om man vinner dubblas poängen.
        public bool Double()
        {
            DoubleInitiated = true;
            return DoubleInitiated;
        }
        public bool DoubleSplit()
        {
            DoubleInitiatedSplit = true;
            return DoubleInitiatedSplit;
        }

        //Kod som ska hoppa igång efter User är klar med sin runda.
        public void DealersTurn()
        {
            //while (CalculateHandValueDealer() <= 16)
            //{        
            //        //DealCardDealer();
            //}
            if (CalculateHandValueDealer() >= 22)
            {
                if (CalculateHandValueUser() == 0)
                {
                    MessageBox.Show("Your first hand points have already been calculated.");
                }
                if (CalculateHandValueUser() > 0 && DoubleInitiated == true)
                {
                    totalScore += 2;
                    MessageBox.Show("The dealer busted! YOU WIN!");
                }
                else if (CalculateHandValueUser() > 0)
                {
                    totalScore += 1;
                    MessageBox.Show("The dealer busted! YOU WIN!");
                }
                if (UserHasSplit == true)
                {
                    if (CalculateHandValueUserSplit() == 0)
                    {
                        MessageBox.Show("Your second hand points have already been calculated.");
                    }
                    if (CalculateHandValueUserSplit() > 0 && DoubleInitiated == true)
                    {
                        totalScore += 2;
                        MessageBox.Show("The dealer busted! YOU WIN YOUR SECOND HAND!");
                    }
                    else if (CalculateHandValueUserSplit() > 0)
                    {
                        totalScore += 1;
                        MessageBox.Show("The dealer busted! YOU WIN YOUR SECOND HAND!");
                    }
                }
            }
            else if (CalculateHandValueDealer() < CalculateHandValueUser())
            {
                if (CalculateHandValueUser() == 0)
                {
                    MessageBox.Show("You already lost points on your 1st hand.");         
                }
                if (CalculateHandValueUser() > 0 && DoubleInitiated == true)
                {
                    totalScore += 2;
                    MessageBox.Show("You Managed to WIN!");
                }
                else if (CalculateHandValueUser() > 0)
                {
                    totalScore += 1;
                    MessageBox.Show("You Managed to WIN!");
                }
                //if (UserHasSplit == true)
                //{

                //}            
            }
            else if (CalculateHandValueDealer() > CalculateHandValueUser())
            {
                if (CalculateHandValueUser() == 0)
                {
                    MessageBox.Show("Already calculated the point for your first hand.");

                    if (CalculateHandValueDealer() < CalculateHandValueUserSplit())
                    {
                        if (CalculateHandValueUserSplit() == 0)
                        {
                            MessageBox.Show("You already lost points on your second hand.");
                        }
                        if (CalculateHandValueUserSplit() > 0 && DoubleInitiatedSplit == true)
                        {
                            totalScore += 2;
                            MessageBox.Show("You Managed to WIN YOUR SECOND HAND!");
                        }
                        else if (CalculateHandValueUserSplit() > 0)
                        {
                            totalScore += 1;
                            MessageBox.Show("You Managed to WIN YOUR SECOND HAND!");
                        }
                    }
                }
                if (CalculateHandValueUser() > 0 && DoubleInitiated == true)
                {
                    totalScore -= 2;
                    MessageBox.Show("The dealer won by a smidge!");
                }
                else if (CalculateHandValueUser() > 0)
                {
                    totalScore -= 1;
                    MessageBox.Show("The dealer won by a smidge!");
                }
                //if (UserHasSplit == true)
                //{

                //}
            }
            else if (CalculateHandValueDealer() == CalculateHandValueUser())
            {
                if (DoubleInitiated == true)
                {
                    MessageBox.Show("Woah! Even Steven! Let's go again!");
                }
                else
                {
                    MessageBox.Show("Woah! Even Steven! Let's go again!");
                }
                //if (UserHasSplit == true)
                //{

                //}
            }
            if (UserHasSplit == true)
            {
                if (CalculateHandValueDealer() == CalculateHandValueUserSplit())
                {
                    if (DoubleInitiated == true)
                    {
                        MessageBox.Show("Woah! Even Steven on your second hand! Let's go again!");
                    }
                    else
                    {
                        MessageBox.Show("Woah! Even Steven on you second hand! Let's go again!");
                    }
                }
                if (CalculateHandValueDealer() < CalculateHandValueUserSplit())
                {
                    if (CalculateHandValueUserSplit() == 0)
                    {
                        MessageBox.Show("You already lost points on your second hand.");
                    }
                    if (CalculateHandValueUserSplit() > 0 && DoubleInitiatedSplit == true)
                    {
                        totalScore += 2;
                        MessageBox.Show("You Managed to WIN YOUR SECOND HAND!");
                    }
                    else if (CalculateHandValueUserSplit() > 0)
                    {
                        totalScore += 1;
                        MessageBox.Show("You Managed to WIN YOUR SECOND HAND!");
                    }
                }
                if (CalculateHandValueUserSplit() == 0)
                {
                    MessageBox.Show("Already calculated the points for your second hand.");
                }
                if (CalculateHandValueDealer() > CalculateHandValueUserSplit() && CalculateHandValueDealer() <=21)
                {
                    if (CalculateHandValueUserSplit() > 0 && DoubleInitiatedSplit == true)
                    {
                        totalScore -= 2;
                        MessageBox.Show("The dealer won by a smidge!YOU LOSE YOUR SECOND HAND!");
                    }
                    else if (CalculateHandValueUserSplit() > 0)
                    {
                        totalScore -= 1;
                        MessageBox.Show("The dealer won by a smidge!YOU LOSE YOUR SECOND HAND!");
                    }
                }
            }           
            RoundEnd();
        }
        //PointsEarned();
        
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
            foreach (Player player in PlayerList)
            {
                if (currentUser == player.Name && totalScore > player.HighScore)
                {
                    player.HighScore = totalScore;
                }
            }
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
            UserCardsSplit.Clear();
            DealerCards.Clear();
            UserHasSplit = false;
            CardList = new CardDeck().CardList;
        }

        //Function som kan användas för att hitta Aces i UserCards eller DealerCards. Nu används Lambda expression istället.
        //bool IsAceCard(Cards card)
        //{
        //    return card.ID.Contains("Ace");
        //}
        
    }
}