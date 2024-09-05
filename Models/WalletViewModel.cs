using Microsoft.AspNetCore.Mvc.Rendering;
using NemoTravel_TestTask;

namespace TestTask_Nemo.Models
{
    public class WalletViewModel
    {
        public decimal Balance;

        public string CurrentCurrencyName;

        public WalletViewModel() { }
        public WalletViewModel(decimal balance, string currentCurrency) 
        {
            this.Balance = balance;
            this.CurrentCurrencyName = currentCurrency;
        }
    }
}
