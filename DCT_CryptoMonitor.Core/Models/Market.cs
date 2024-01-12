namespace DCT_CryptoMonitor.Core.Models;

public class Market
{
    public string ExchangeId { get; set; } = string.Empty;
    public string BaseId { get; set; } = string.Empty;
    public string BaseSymbol { get; set; } = string.Empty;
    public string QuoteId { get; set; } = string.Empty;
    public string QuoteSymbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Volume24H { get; set; }
}