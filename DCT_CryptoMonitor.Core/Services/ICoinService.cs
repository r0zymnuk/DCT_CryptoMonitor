using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Models.Enums;

namespace DCT_CryptoMonitor.Core.Services;

public interface ICoinService
{
    public Task<bool> Ping();
    // public Task<List<string>> GetSupportedVsCurrencies();
    public Task<List<Coin>> GetTopMarketCapCoins(int count = 100, string currency = "usd");
    public Task<Coin> GetCoinById(string id, string currency = "usd");
    public Task<List<PriceHistory>> GetPriceHistory(string id, DateTime start, DateTime end, PriceInterval interval = PriceInterval.h1);
}