using DCT_CryptoMonitor.Desktop.MVVM.Model;
using DCT_CryptoMonitor.Desktop.MVVM.Model.Enums;

namespace DCT_CryptoMonitor.Desktop.Services.Coins;

public interface ICoinService
{
    public Task<bool> Ping();
    // public Task<List<string>> GetSupportedVsCurrencies();
    public Task<List<Coin>> GetTopMarketCapCoins(int count = 100, string currency = "usd");
    public Task<Coin?> GetCoinById(string id, string currency = "usd");
    public Task<List<PriceHistory>> GetPriceHistory(string id, DateTime start, DateTime end, PriceInterval interval = PriceInterval.h1);
    Task<List<Market>> GetMarkets(string coinId, int limit = 100, int offset = 0);
    Task<List<Exchange>> GetExchanges();
    Task<Exchange?> GetExchangeById(string id);
}