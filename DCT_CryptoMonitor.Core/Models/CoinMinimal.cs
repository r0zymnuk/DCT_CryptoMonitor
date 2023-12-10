namespace DCT_CryptoMonitor.Core.Models;

public class CoinMinimal
{
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double CurrentPrice { get; set; }
    public double MarketCap { get; set; }
    public int MarketCapRank { get; set; }
    public double PriceChangePercentage24H { get; set; }
}