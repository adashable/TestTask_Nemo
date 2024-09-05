using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public class WalletService : IWalletService
    {
        private Currency _currency { get; set; }
        private decimal _amount { get; set ; }

        public WalletService() { }

        public bool ChangeBalance(decimal amount)
        {
            this._amount = amount; 
            return true;

        }

        public bool ChangeCurrency(Currency currency)
        {
            if(currency == null)
                return false;
            this._currency = currency;
            return true;
        }
        public decimal GetBalance()
        {
            return this._amount;
        }

        public Currency GetCurrentCurrency()
        {
            return this._currency;
        }

        public bool ConvertTo(int currencyNumCode)
        {

            
            return true;
        }

    }
}
