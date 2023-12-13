namespace DCT_CryptoMonitor.Core.Models;

public class CoinMinimal
{
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal MarketCap { get; set; }
    public int MarketCapRank { get; set; }
    public decimal PriceChangePercentage24H { get; set; }
    public decimal Volume24H { get; set; }
    public decimal Supply { get; set; }
}