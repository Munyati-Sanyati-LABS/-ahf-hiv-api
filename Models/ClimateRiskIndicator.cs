namespace AhfHivApi.Models;

public class ClimateRiskIndicator
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public int Year { get; set; }
    public double AvgTempCelsius { get; set; }
    public double RainfallAnomalyPct { get; set; }
    public int FloodEvents { get; set; }
    public int DroughtEvents { get; set; }
    public double ClimateVulnerabilityIndex { get; set; }
    public int ExtremeHeatDays { get; set; }

    // Foreign key
    public Country? Country { get; set; }
}