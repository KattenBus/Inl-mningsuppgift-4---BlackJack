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
        public string ID { get; set; }
        public int Value { get; set; }
        public string ImagePathFront { get; set; }
        public string ImagePathBack { get; set; }

        public Cards(string iD, int value, string imagePathFront, string imagePathBack)
        {
            ID = iD;
            Value = value;
            ImagePathFront = imagePathFront;
            ImagePathBack = imagePathBack;
        }
    }
}
