using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public class CurrencyCalculatorService : ICurrencyCalculatorService
    {
        public CurrencyCalculatorService(ICurrencyConversionService currencyConversionService)
        {
            this._currencyConversionService = currencyConversionService ?? throw new ArgumentNullException(nameof(currencyConversionService)); 
        }
        private ICurrencyConversionService _currencyConversionService;
        
        public decimal AddAmount(decimal balance, Currency currencyTo, decimal amount, Currency currencyFrom)
        {
            decimal res;
            try
            {
                if (currencyFrom == currencyTo)
                    res = balance + amount;
                else
                {
                    res = balance + this._currencyConversionService.ConvertCurrency(amount, currencyFrom, currencyTo);
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public decimal WithdrawAmount(decimal balance, Currency currencyTo, decimal amount, Currency currencyFrom)
        {
            decimal res;
            try
            {
                if (currencyFrom == currencyTo)
                    res = balance - amount;
                else
                {
                    res = balance - this._currencyConversionService.ConvertCurrency(amount, currencyFrom, currencyTo);
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

    }
}
