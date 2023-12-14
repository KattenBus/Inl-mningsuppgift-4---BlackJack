using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppInlämning_4___BlackJack
{
    public class Player
    {
        public int HighScore { get; set; }
        public string Name { get; set; }
        public Player(int highScore, string name)
        {
            this.HighScore = highScore;
            this.Name = name;
        }

        public string GetCSV()
        {
            return Name + "," + HighScore;
        }
    }
}
