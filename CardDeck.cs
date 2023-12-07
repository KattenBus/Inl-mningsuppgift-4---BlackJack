using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppInlämning_4___BlackJack
{
    public class CardDeck
    {
        //Listan som alla kort ligger i.
        public List<Cards> CardList = new List<Cards>();
        public CardDeck()
        {
            //Hearts 2, Hearts 3 osv är ID för varje kort, nummer 2, 3, 4 osv är value, imagepath är den sista koden.
            //Koden: Path.Combine(AppDomain.CurrentDomain.BaseDirectory funkar så att programmet söker upp mappen cards och sedan vilken fil som ligger i den mappen,
            //oavsett vilken dator mappen ligger på. Den skapar en sökväg dit helt enkelt. Den utgår från .exe filen för programmet, vilken ligger i mappen bin/debugg/net6.0-windows.
            //Där ligger även Cards mappen med alla bilder. GetParentDirectory.
            CardList.Add(new Cards("Hearts 2", 2, "Cards/2-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 3", 3, "Cards/3-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 4", 4, "Cards/4-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 5", 5, "Cards/5-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 6", 6, "Cards/6-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 7", 7, "Cards/7-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 8", 8, "Cards/8-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 9", 9, "Cards/9-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts 10", 10, "Cards/10-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts Jack", 10, "Cards/J-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts Queen", 10, "Cards/Q-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts King", 10, "Cards/K-H.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Hearts Ace", 11, "Cards/A-H.png", "Cards/BACK.png"));

            CardList.Add(new Cards("Diamonds 2", 2, "Cards/2-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 3", 3, "Cards/3-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 4", 4, "Cards/4-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 5", 5, "Cards/5-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 6", 6, "Cards/6-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 7", 7, "Cards/7-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 8", 8, "Cards/8-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 9", 9, "Cards/9-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds 10", 10, "Cards/10-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds Jack", 10, "Cards/J-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds Queen", 10, "Cards/Q-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds King", 10, "Cards/K-D.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Diamonds Ace", 11, "Cards/A-D.png", "Cards/BACK.png"));

            CardList.Add(new Cards("Spades 2", 2, "Cards/2-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 3", 3, "Cards/3-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 4", 4, "Cards/4-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 5", 5, "Cards/5-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 6", 6, "Cards/6-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 7", 7, "Cards/7-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 8", 8, "Cards/8-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 9", 9, "Cards/9-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades 10", 10, "Cards/10-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades Jack", 10, "Cards/J-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades Queen", 10, "Cards/Q-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades King", 10, "Cards/K-S.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Spades Ace", 11, "Cards/A-S.png", "Cards/BACK.png"));

            CardList.Add(new Cards("Clover 2", 2, "Cards/2-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 3", 3, "Cards/3-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 4", 4, "Cards/4-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 5", 5, "Cards/5-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 6", 6, "Cards/6-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 7", 7, "Cards/7-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 8", 8, "Cards/8-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 9", 9, "Cards/9-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover 10", 10, "Cards/10-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover Jack", 10, "Cards/J-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover Queen", 10, "Cards/Q-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover King", 10, "Cards/K-C.png", "Cards/BACK.png"));
            CardList.Add(new Cards("Clover Ace", 11, "Cards/A-C.png", "Cards/BACK.png"));
        }
    }
}