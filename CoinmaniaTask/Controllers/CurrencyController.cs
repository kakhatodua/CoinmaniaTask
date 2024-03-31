using CoinmaniaTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoinmaniaTask.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly CoinmaniaDbContext _dbContext;

        public CurrencyController(CoinmaniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetCurrency(bool sortByPrice, bool sortByBase)
        {
            if (sortByPrice)
            {
                var sortedByPrice = _dbContext.Currencies.OrderBy(x => x.Price).ToList();

                return View(sortedByPrice);
            }

            if (sortByBase)
            {
                var sortedByBase = _dbContext.Currencies.OrderBy(x => x.BaseSymbol).ToList();

                return View(sortedByBase);
            }

            var currencies = _dbContext.Currencies.ToList();
            return View(currencies);
        }

        public IActionResult GetCurrencyHistory(string currency)
        {
            var currencies = _dbContext.CurrencyHistories.Where(x => x.Base == currency && x.CreateDate < DateTime.Now.AddMinutes(-60)).ToList();

            return View(currencies);
        }
    }
}