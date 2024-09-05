using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NemoTravel_TestTask
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        public CurrencyConversionService(ICurrencyRatesXmlService currencyRatesXmlService) 
        {
            this._currencyRatesXmlService = currencyRatesXmlService ?? throw new ArgumentNullException(nameof(currencyRatesXmlService)); 
        }

        private ICurrencyRatesXmlService _currencyRatesXmlService;
        
        public decimal ConvertCurrency(decimal amount, Currency currencyFrom, Currency currencyTo) 
        {
            if (currencyFrom == null || currencyTo == null)
                return 0;
            decimal res = amount * this._currencyRatesXmlService.GetCurrentExchangeRate(currencyFrom) / this._currencyRatesXmlService.GetCurrentExchangeRate(currencyTo);
            return Math.Round(res, 2);
        }

    }
}
