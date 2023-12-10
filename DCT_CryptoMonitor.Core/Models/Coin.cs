namespace DCT_CryptoMonitor.Core.Models;

public class Coin : CoinMinimal
{
    public double FullyDilutedValuation { get; set; }
    public double TotalVolume { get; set; }
    public double High24H { get; set; }
    public double Low24H { get; set; }
    public double PriceChange24H { get; set; }
    public double MarketCapChange24H { get; set; }
    public double MarketCapChangePercentage24H { get; set; }
    public double CirculatingSupply { get; set; }
    public double TotalSupply { get; set; }
    public double Ath { get; set; }
    public double AthChangePercentage { get; set; }
    public DateTime AthDate { get; set; }
    public double Atl { get; set; }
    public double AtlChangePercentage { get; set; }
    public DateTime AtlDate { get; set; }
    public DateTime LastUpdated { get; set; }
}