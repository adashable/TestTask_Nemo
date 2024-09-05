using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemoTravel_TestTask
{
    public interface ICurrencyRatesXmlService
    {
        public decimal GetCurrentExchangeRate(Currency currency);
        public Currency GetBaseCurrency();
        public List<SelectListItem> GetAvailibleCurrenciesList();
        public Currency GetCurrencyByNumCode(int numCode);
    }
}
