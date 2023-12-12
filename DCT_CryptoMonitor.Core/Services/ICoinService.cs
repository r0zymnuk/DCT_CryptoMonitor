using DCT_CryptoMonitor.Core.Models;

namespace DCT_CryptoMonitor.Core.Services;

public interface ICoinService
{
    public Task<bool> Ping();
    // public Task<List<string>> GetSupportedVsCurrencies();
    public Task<List<CoinMinimal>> GetTopMarketCapCoins(int count = 100, string currency = "usd");
    public Task<Coin> GetCoinById(string id, string currency = "usd");
}