using System.Text.Json;
using System.Web;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Services;

namespace DCT_CryptoMonitor.Infrastructure.Services;

public class CoinGeckoClient : ICoinService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public CoinGeckoClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _httpClient.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
        // ping
        var ping = GetAsync("ping").Result;
        
        ping.EnsureSuccessStatusCode();
    }
    
    private Task<HttpResponseMessage> GetAsync(string url)
    {
        var uri = new Uri(_httpClient.BaseAddress!, url);
        
        var query = HttpUtility.ParseQueryString(uri.Query);
        query["x_cg_demo_api_key"] = _apiKey;
        
        uri = new UriBuilder(uri) {Query = query.ToString()}.Uri;
        
        return _httpClient.GetAsync(uri);
    }

    public async Task<bool> Ping()
    {
        var response = await GetAsync("ping");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<string>> GetSupportedVsCurrencies()
    {
        var response = await GetAsync("simple/supported_vs_currencies");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(content) ?? new List<string>();
    }

    public async Task<List<CoinMinimal>> GetTopMarketCapCoins(int count = 100, string currency = "usd")
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["vs_currency"] = currency;
        query["order"] = "market_cap_desc";
        query["per_page"] = count.ToString();
        query["page"] = "1";
        query["sparkline"] = "false";
        query["precision"] = "2";
        
        var response = await GetAsync($"coins/markets?{query}");
        if (!response.IsSuccessStatusCode)
        {
            return new List<CoinMinimal>();
        }
        
        var content = await response.Content.ReadAsStringAsync();
        var coins = JsonSerializer.Deserialize<List<CoinMinimal>>(content);
        return coins ?? new List<CoinMinimal>();
    }

    public async Task<Coin> GetCoinById(string id, string currency = "usd")
    {
        throw new NotImplementedException();
    }
}