using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public interface ICurrencyConversionService
    {
        public decimal ConvertCurrency(decimal amount, Currency currencyFrom, Currency currencyTo);
    }
}
