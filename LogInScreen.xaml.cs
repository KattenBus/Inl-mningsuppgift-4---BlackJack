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
    /// Interaction logic for LogInScreen.xaml
    /// </summary>
    public partial class LogInScreen : Window
    {
        public LogInScreen()
        {
            InitializeComponent();
        }
        //Startup Screen, där programmet börjar är just nu satt till GameMenu, nu innan Inloggningen fungerar.
        //För att byta gå till: App.xaml i solution explorer -> Byt StartupUri="GameMenu.xaml"> till StartupUri="LogInScreen.xaml">.

    }
}
