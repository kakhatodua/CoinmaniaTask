using System;
using System.ComponentModel.DataAnnotations;

namespace CoinmaniaTask.Models
{
    public class CurrencyHistory
    {
        [Key]
        public int Id { get; set; }

        public string Base { get; set; }
        public string BaseSymbol { get; set; }
        public string Quote { get; set; }
        public string QuoteSymbol { get; set; }
        public double Price { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
