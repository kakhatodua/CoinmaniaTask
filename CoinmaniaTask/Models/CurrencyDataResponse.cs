using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoinmaniaTask.Models
{
    public class CurrencyDataResponse
    {
        public int StatusCode { get; set; }
        public List<Currency> Data { get; set; }
        public object Message { get; set; }
    }
}
