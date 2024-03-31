using CoinmaniaTask.Models;

public class CurrencyTask : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly HttpClient _httpClient;

    public CurrencyTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://coinmania.ge/api/v2/assets/prices/?assetId=gel")
        };
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000 * 10);

            using IServiceScope scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CoinmaniaDbContext>();
            var response = await _httpClient.GetFromJsonAsync<CurrencyDataResponse>(string.Empty);

            var currencies = response.Data.Select(x => new Currency
            {
                Base = x.Base,
                BaseSymbol = x.BaseSymbol,
                BuyPrice = x.BuyPrice,
                LastUpdateDate = DateTime.Now,
                Price = x.Price,
                Quote = x.Quote,
                QuoteSymbol = x.QuoteSymbol,
                SellPrice = x.SellPrice,
            }).ToList();

            var currencyHistory = response.Data.Select(x => new CurrencyHistory
            {
                Base = x.Base,
                BaseSymbol = x.BaseSymbol,
                BuyPrice = x.BuyPrice,
                CreateDate = DateTime.Now,
                Price = x.Price,
                Quote = x.Quote,
                QuoteSymbol = x.QuoteSymbol,
                SellPrice = x.SellPrice,
            }).ToList();

            dbContext.CurrencyHistories.AddRange(currencyHistory);
            dbContext.SaveChanges();


            var dbCurrencies = dbContext.Currencies.ToList();
            dbContext.Currencies.RemoveRange(dbCurrencies);

            dbContext.Currencies.AddRange(currencies);
            dbContext.SaveChanges();

        }
    }
}