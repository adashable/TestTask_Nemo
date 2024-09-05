using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public interface ICurrencyCalculatorService
    {
        public decimal AddAmount(decimal balance, Currency currencyTo, decimal amount, Currency currencyFrom);
        public decimal WithdrawAmount(decimal balance, Currency currencyTo, decimal amount, Currency currencyFrom);
    }
}
