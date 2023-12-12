using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppInlämning_4___BlackJack
{
    public partial class UserBalance
    {
        public string Username { get; set; }
        int Balance { get; set; }

        public UserBalance(string username, int balance)
        {
            this.Username = username;
            this.Balance = balance;
        }

        public void AddBalance(int depositAmount)
        {
            Balance += depositAmount;
        }

        public bool RemoveBalance(int withdrawAmount)
        {
            if (Balance > withdrawAmount)
            {
                Balance -= withdrawAmount;
                return true;
            }

            return false;
        }
    }
}
