namespace DCT_CryptoMonitor.Core.Models;

public class Exchange
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Rank { get; set; }
    public decimal PercentTotalVolume { get; set; }
    public decimal Volume { get; set; }
    public int TradingPairs { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime Updated { get; set; }
}