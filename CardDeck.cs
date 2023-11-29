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
        public CardDeck() 
        {
            //Listan som alla kort ligger i.
            List <Cards> CardList = new List<Cards>();

            //Hearts 2, Hearts 3 osv är ID för varje kort, nummer 2, 3, 4 osv är value, imagepath är den sista koden.
            //Koden: Path.Combine(AppDomain.CurrentDomain.BaseDirectory funkar så att programmet söker upp mappen cards och sedan vilken fil som ligger i den mappen,
            //oavsett vilken dator mappen ligger på. Den skapar en sökväg dit helt enkelt. Den utgår från .exe filen för programmet, vilken ligger i mappen bin/debugg/net6.0-windows.
            //Där ligger även Cards mappen med alla bilder.
            CardList.Add(new Cards("Hearts 2", 2, Path.GetFileName("Cards/2 - H.png")));
            CardList.Add(new Cards("Hearts 3", 3, Path.GetFileName("Cards /3 - H.png")));
            CardList.Add(new Cards("Hearts 4", 4, Path.GetFileName("Cards /4 - H.png")));
            CardList.Add(new Cards("Hearts 5", 5, Path.GetFileName("Cards /5 - H.png")));
            CardList.Add(new Cards("Hearts 6", 6, Path.GetFileName("Cards /6 - H.png")));
            CardList.Add(new Cards("Hearts 7", 7, Path.GetFileName("Cards /7 - H.png")));
            CardList.Add(new Cards("Hearts 8", 8, Path.GetFileName("Cards /8 - H.png")));
            CardList.Add(new Cards("Hearts 9", 9, Path.GetFileName( "Cards /9 - H.png")));
            CardList.Add(new Cards("Hearts 10", 10, Path.GetFileName("Cards /10 - H.png)")));
            CardList.Add(new Cards("Hearts Jack", 10, Path.GetFileName( "Cards /J - H.png")));
            CardList.Add(new Cards("Hearts Queen", 10, Path.GetFileName("Cards /Q - H.png")));
            CardList.Add(new Cards("Hearts King", 10, Path.GetFileName("Cards /K - H.png")));
            CardList.Add(new Cards("Hearts Ace", 1, Path.GetFileName("Cards /A - H.png")));

            CardList.Add(new Cards("Diamonds 2", 2, Path.GetFileName("Cards /2 - D.png")));
            CardList.Add(new Cards("Diamonds 3", 3, Path.GetFileName("Cards /3 - D.png")));
            CardList.Add(new Cards("Diamonds 4", 4, Path.GetFileName("Cards /4 - D.png")));
            CardList.Add(new Cards("Diamonds 5", 5, Path.GetFileName("Cards /5 - D.png")));
            CardList.Add(new Cards("Diamonds 6", 6, Path.GetFileName("Cards /6 - D.png")));
            CardList.Add(new Cards("Diamonds 7", 7, Path.GetFileName("Cards /7 - D.png")));
            CardList.Add(new Cards("Diamonds 8", 8, Path.GetFileName("Cards /8 - D.png")));
            CardList.Add(new Cards("Diamonds 9", 9, Path.GetFileName("Cards /9 - D.png")));
            CardList.Add(new Cards("Diamonds 10", 10, Path.GetFileName("Cards /10 - D.png")));
            CardList.Add(new Cards("Diamonds Jack", 10, Path.GetFileName("Cards /J - D.png")));
            CardList.Add(new Cards("Diamonds Queen", 10, Path.GetFileName("Cards /Q - D.png")));
            CardList.Add(new Cards("Diamonds King", 10, Path.GetFileName("Cards /K - D.png")));
            CardList.Add(new Cards("Diamonds Ace", 1, Path.GetFileName("Cards /A - D.png")));

            CardList.Add(new Cards("Spades 2", 2, Path.GetFileName("Cards /2 - S.png")));
            CardList.Add(new Cards("Spades 3", 3, Path.GetFileName("Cards /3 - S.png")));
            CardList.Add(new Cards("Spades 4", 4, Path.GetFileName("Cards /4 - S.png")));
            CardList.Add(new Cards("Spades 5", 5, Path.GetFileName("Cards /5 - S.png")));
            CardList.Add(new Cards("Spades 6", 6, Path.GetFileName("Cards /6 - S.png")));
            CardList.Add(new Cards("Spades 7", 7, Path.GetFileName("Cards /7 - S.png")));
            CardList.Add(new Cards("Spades 8", 8, Path.GetFileName("Cards /8 - S.png")));
            CardList.Add(new Cards("Spades 9", 9, Path.GetFileName("Cards /9 - S.png")));
            CardList.Add(new Cards("Spades 10", 10, Path.GetFileName("Cards /10 - S.png")));
            CardList.Add(new Cards("Spades Jack", 10, Path.GetFileName("Cards /J - S.png")));
            CardList.Add(new Cards("Spades Queen", 10, Path.GetFileName("Cards /Q - S.png")));
            CardList.Add(new Cards("Spades King", 10, Path.GetFileName("Cards /K - S.png")));
            CardList.Add(new Cards("Spades Ace", 1, Path.GetFileName("Cards /A - S.png")));

            CardList.Add(new Cards("Clover 2", 2, Path.GetFileName("Cards /2 - C.png")));
            CardList.Add(new Cards("Clover 3", 3, Path.GetFileName("Cards /3 - C.png")));
            CardList.Add(new Cards("Clover 4", 4, Path.GetFileName("Cards /4 - C.png")));
            CardList.Add(new Cards("Clover 5", 5, Path.GetFileName("Cards /5 - C.png")));
            CardList.Add(new Cards("Clover 6", 6, Path.GetFileName("Cards /6 - C.png")));
            CardList.Add(new Cards("Clover 7", 7, Path.GetFileName("Cards /7 - C.png")));
            CardList.Add(new Cards("Clover 8", 8, Path.GetFileName("Cards /8 - C.png")));
            CardList.Add(new Cards("Clover 9", 9, Path.GetFileName("Cards /9 - C.png")));
            CardList.Add(new Cards("Clover 10", 10, Path.GetFileName("Cards /10 - C.png")));
            CardList.Add(new Cards("Clover Jack", 10, Path.GetFileName("Cards /J - C.png")));
            CardList.Add(new Cards("Clover Queen", 10, Path.GetFileName("Cards /Q - C.png")));
            CardList.Add(new Cards("Clover King", 10, Path.GetFileName("Cards /K - C.png")));
            CardList.Add(new Cards("Clover Ace", 1, Path.GetFileName("Cards /A - C.png")));
        }   
    }
}
