using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NemoTravel_TestTask;
using TestTask_Nemo.Models;

namespace TestTask_Nemo.Controllers
{
    public class WalletController : Controller
    {
        public WalletController(ILogger<WalletController> logger, ICurrencyCalculatorService currencyCalculatorService,
            ICurrencyConversionService currencyConversionService, ICurrencyRatesXmlService currencyRatesXmlService,
            IWalletService walletService) 
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._currencyCalculatorService = currencyCalculatorService ?? throw new ArgumentNullException(nameof(currencyCalculatorService));
            this._currencyConversionService = currencyConversionService ?? throw new ArgumentNullException(nameof(currencyConversionService));
            this._currencyRatesXmlService = currencyRatesXmlService ?? throw new ArgumentNullException(nameof(currencyRatesXmlService));
            this._walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        }
        private readonly ILogger<WalletController> _logger;
        private readonly ICurrencyCalculatorService _currencyCalculatorService;
        private readonly ICurrencyConversionService _currencyConversionService;
        private readonly ICurrencyRatesXmlService _currencyRatesXmlService;
        private readonly IWalletService _walletService;


        public ActionResult Index()
        {
            decimal balance = _walletService.GetBalance();
            Currency currentCurrency = _walletService.GetCurrentCurrency();
            if(currentCurrency == null)
            {
                currentCurrency = this._currencyRatesXmlService.GetBaseCurrency();
                this._walletService.ChangeCurrency(currentCurrency);
            }

            List<SelectListItem> list = _currencyRatesXmlService.GetAvailibleCurrenciesList();
            ViewBag.CurrenciesList = list;
            return View(new WalletViewModel(balance, currentCurrency.Name));
        }

        [HttpPost]
        public ActionResult Convert(int numCode)
        {
            Currency cur = this._currencyRatesXmlService.GetCurrencyByNumCode(numCode);
            var newBalance = this._currencyConversionService.ConvertCurrency(this._walletService.GetBalance(), 
                this._walletService.GetCurrentCurrency(),
                cur );
            this._walletService.ChangeBalance(newBalance);
            this._walletService.ChangeCurrency(cur);
            List<SelectListItem> list = _currencyRatesXmlService.GetAvailibleCurrenciesList();
            ViewBag.CurrenciesList = list;

            return View(new WalletViewModel(newBalance, cur.Name));
        }

        [HttpPost]
        public ActionResult Add(int numCode, int amount)
        {
            Currency cur = this._currencyRatesXmlService.GetCurrencyByNumCode(numCode);
            Currency currentCurrency = this._walletService.GetCurrentCurrency();

            var newBalance = this._currencyCalculatorService.AddAmount(this._walletService.GetBalance(),
                currentCurrency, amount, cur);
            this._walletService.ChangeBalance(newBalance);
            List<SelectListItem> list = _currencyRatesXmlService.GetAvailibleCurrenciesList();
            ViewBag.CurrenciesList = list;

            return View(new WalletViewModel(newBalance, currentCurrency.Name));
        }

        [HttpPost]
        public ActionResult Withdraw(int numCode, decimal amount)
        {
            Currency cur = this._currencyRatesXmlService.GetCurrencyByNumCode(numCode);
            decimal balance = this._walletService.GetBalance();
            Currency currentCurrency = this._walletService.GetCurrentCurrency();

            var newBalance = this._currencyCalculatorService.WithdrawAmount(balance,
                currentCurrency, amount, cur);
            List<SelectListItem> list = _currencyRatesXmlService.GetAvailibleCurrenciesList();
            ViewBag.CurrenciesList = list;

            if (newBalance < 0) 
            {
                ViewBag.Message = "Для операции недостаточно средств на счёте :(";
                ViewBag.MemeSrc = "/lib/meme.jpg";
                return View(new WalletViewModel(balance, currentCurrency.Name));

            }
            else
                this._walletService.ChangeBalance(newBalance);

            return View(new WalletViewModel(newBalance, currentCurrency.Name));
        }

    }
}
