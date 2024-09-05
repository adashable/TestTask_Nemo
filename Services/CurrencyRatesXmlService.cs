using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NemoTravel_TestTask
{
    public class CurrencyRatesXmlService : ICurrencyRatesXmlService
    {
        public CurrencyRatesXmlService() { }
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        private Currency _baseCurrency = new Currency(643, "RUB", 1);
        List<Currency> _currencies;
        private const string _currenciesXmlUrl = @"https://www.cbr-xml-daily.ru/daily_utf8.xml";


        public decimal GetCurrentExchangeRate(Currency currency)
        {
            if (currency == null)
                return 0;
            //this.UpdateCurrencyRates();

            return this._currencies.Where(c => c.Name.Equals(currency.Name)).First().ExchangeRate;
        }
        public void UpdateCurrencyRates()
        {
            this._currencies = new List<Currency>()
            {
                this._baseCurrency
            };
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(_currenciesXmlUrl);
                if (document == null)
                    throw new Exception("Ссылка недействительна.\n");
                foreach (XmlNode node in document.DocumentElement)
                {
                    if (node.Name == "Valute")
                    {
                        string currencyName = "";
                        decimal currencyRate = 0;
                        int currencyNumCode = 0;
                        foreach (XmlElement element in node.ChildNodes)
                        {
                            if (element.Name == "NumCode")
                                int.TryParse(element.InnerText, out currencyNumCode);

                            if (element.Name == "CharCode")
                                currencyName = element.InnerText;

                            if (element.Name == "VunitRate")
                                decimal.TryParse(element.InnerText.Replace(',', '.'), out currencyRate);
                        }
                        _currencies.Add(new Currency(currencyNumCode, currencyName, currencyRate));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить курсы валют. \n" + ex);
            }
        }

        public Currency GetBaseCurrency()
        {
            return this._baseCurrency;
        }

        public List<SelectListItem> GetAvailibleCurrenciesList()
        {
            List<Currency> currencies = new List<Currency>();
            if(this._currencies == null)
                this.UpdateCurrencyRates();
            currencies.AddRange(this._currencies);   
            List<SelectListItem> currencyList = currencies.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.NumCode.ToString(),
                    Selected = false
                };
            });

            return currencyList;
        }

        public Currency GetCurrencyByNumCode(int numCode)
        {
            this.UpdateCurrencyRates();
            return this._currencies.Where(c => c.NumCode.Equals(numCode)).First();
        }
    }
}
