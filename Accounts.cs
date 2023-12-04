using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppInlämning_4___BlackJack
{
    public class Accounts
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Accounts(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
