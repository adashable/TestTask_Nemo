using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public record Currency
    {
        public int NumCode;
        public string Name;
        public decimal ExchangeRate;
        public Currency(int numCode, string name, decimal exchangeRate)
        {
            this.NumCode = numCode;  
            this.Name = name;
            this.ExchangeRate = exchangeRate;
        }
    }
    public interface IWalletService
    {
        public bool ChangeBalance(decimal amount);
        public bool ChangeCurrency(Currency currency);
        public decimal GetBalance();
        public Currency GetCurrentCurrency();
    }
}
