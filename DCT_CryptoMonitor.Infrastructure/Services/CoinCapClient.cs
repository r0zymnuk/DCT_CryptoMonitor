using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Web;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Infrastructure.Configurations;

namespace DCT_CryptoMonitor.Infrastructure.Services;

public class CoinCapClient : ICoinService
{
    private readonly HttpClient _httpClient;

    public CoinCapClient(HttpClient httpClient, ApiOptions apiOptions)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(apiOptions.Url);
        // add bearer token
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiOptions.ApiKey}");
        // ping
        
        if (_httpClient.GetAsync("assets").Result.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("CoinCap ping failed");
        }
    }
    
    private async Task<HttpResponseMessage> GetAsync(string url, NameValueCollection? query = null)
    {
        var uri = new Uri(_httpClient.BaseAddress!, url);
        
        return await _httpClient.GetAsync(new UriBuilder(uri) {Query = query?.ToString()}.Uri);
    }

    public async Task<bool> Ping()
    {
        var response = await GetAsync("assets");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<CoinMinimal>> GetTopMarketCapCoins(int count = 100, string currency = "usd")
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["limit"] = count.ToString();
        
        var response = await GetAsync("assets", query);
        if (!response.IsSuccessStatusCode)
        {
            return new List<CoinMinimal>();
        }
        
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<JsonElement>(content);

        return data.GetProperty("data")
            .EnumerateArray()
            .Select(coin => new CoinMinimal
            {
                Id = coin.GetProperty("id").GetString()!,
                Symbol = coin.GetProperty("symbol").GetString()!,
                Name = coin.GetProperty("name").GetString()!,
                CurrentPrice = ConvertCoinCapToDecimal(coin.GetProperty("priceUsd").GetString()!),
                MarketCap = ConvertCoinCapToDecimal(coin.GetProperty("marketCapUsd").GetString()!),
                MarketCapRank = Convert.ToInt32(coin.GetProperty("rank").GetString()),
                PriceChangePercentage24H = ConvertCoinCapToDecimal(coin.GetProperty("changePercent24Hr").GetString()!),
                Volume24H = ConvertCoinCapToDecimal(coin.GetProperty("volumeUsd24Hr").GetString()!),
                Supply = ConvertCoinCapToDecimal(coin.GetProperty("supply").GetString()!)
            })
            .ToList();
    }

    public async Task<Coin> GetCoinById(string id, string currency = "usd")
    {
        throw new NotImplementedException();
    }
    
    public static decimal ConvertCoinCapToDecimal(string value)
    {
        return value != null ? Convert.ToDecimal(value, CultureInfo.InvariantCulture) : 0;
    }
}