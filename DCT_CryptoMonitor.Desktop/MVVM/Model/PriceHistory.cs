namespace DCT_CryptoMonitor.Desktop.MVVM.Model;
public struct PriceHistory : IComparable<PriceHistory>
{
    public decimal PriceUsd { get; set; }
    public DateTime DateTime { get; set; }

    public override string ToString()
    {
        return $"{DateTime} - {PriceUsd}";
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(PriceHistory other)
    {
        var priceUsdComparison = PriceUsd.CompareTo(other.PriceUsd);
        if (priceUsdComparison != 0) return priceUsdComparison;
        return DateTime.CompareTo(other.DateTime);
    }
}
