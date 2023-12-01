using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace GruppInlämning_4___BlackJack
{
    public class Cards
    {
        //De properties korten har. Varje kort har ett ID för igenkänning, Value för kortets värde i spelet och en Imagepath som länkar till en bild.
        public string ID { get; }
        public int Value { get; }
        public string ImagePathFront { get; }
        public string ImagePathBack { get; }

        public Cards(string iD, int value, string imagePathFront, string imagePathBack)
        {
            ID = iD;
            Value = value;
            ImagePathFront = imagePathFront;
            ImagePathBack = imagePathBack;
        }
    }
}
