using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Web;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Models.Enums;
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

    public async Task<List<Coin>> GetTopMarketCapCoins(int count = 100, string currency = "usd")
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["limit"] = count.ToString();
        
        var response = await GetAsync("assets", query);
        if (!response.IsSuccessStatusCode)
        {
            return new List<Coin>();
        }
        
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<JsonElement>(content).GetProperty("data");

        return data.EnumerateArray()
            .Select(coin => ConvertJsonToCoin(coin))
            .ToList();
    }

    public async Task<Coin?> GetCoinById(string id, string currency = "usd")
    {
        var response = await GetAsync($"assets/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<JsonElement>(content).GetProperty("data");

        return ConvertJsonToCoin(data);
    }

    public async Task<List<PriceHistory>> GetPriceHistory(string id, DateTime start, DateTime end, PriceInterval interval = PriceInterval.h1)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["interval"] = interval.ToString();
        query["start"] = ((DateTimeOffset)start).ToUnixTimeMilliseconds().ToString();
        query["end"] = ((DateTimeOffset)end).ToUnixTimeMilliseconds().ToString();
        
        var response = await GetAsync($"assets/{id}/history", query);
        if (response is not { IsSuccessStatusCode: true })
            return new List<PriceHistory>();
        
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<JsonElement>(content).GetProperty("data");
        return data.EnumerateArray().Select(d => new PriceHistory
        {
            DateTime = DateTimeOffset.FromUnixTimeMilliseconds(d.GetProperty("time").GetInt64()).DateTime,
            PriceUsd = ConvertCoinCapToDecimal(d.GetProperty("priceUsd").GetString()!)
        }).ToList();
    }
    
    public static decimal ConvertCoinCapToDecimal(string value)
    {
        return value != null ? Convert.ToDecimal(value, CultureInfo.InvariantCulture) : 0;
    }

    public static Coin ConvertJsonToCoin(JsonElement json)
    {
        return new Coin
        {
            Id = json.GetProperty("id").GetString()!,
            Symbol = json.GetProperty("symbol").GetString()!,
            Name = json.GetProperty("name").GetString()!,
            CurrentPrice = ConvertCoinCapToDecimal(json.GetProperty("priceUsd").GetString()!),
            MarketCap = ConvertCoinCapToDecimal(json.GetProperty("marketCapUsd").GetString()!),
            MarketCapRank = Convert.ToInt32(json.GetProperty("rank").GetString()),
            PriceChangePercentage24H = ConvertCoinCapToDecimal(json.GetProperty("changePercent24Hr").GetString()!),
            Volume24H = ConvertCoinCapToDecimal(json.GetProperty("volumeUsd24Hr").GetString()!),
            Supply = ConvertCoinCapToDecimal(json.GetProperty("supply").GetString()!),
            TotalSupply = ConvertCoinCapToDecimal(json.GetProperty("maxSupply").GetString()!)
        };
    }
}