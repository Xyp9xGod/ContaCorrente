using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Enums
{
    public class TransactionType
    {
        public enum Type
        {
            Deposit = 1,
            Withdrawl = 2,
            Payment = 3
        }
    }
}
