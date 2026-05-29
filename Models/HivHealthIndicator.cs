namespace AhfHivApi.Models;

public class HivHealthIndicator
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public int Year { get; set; }
    public double HivPrevalencePct { get; set; }
    public double ArtCoveragePct { get; set; }
    public double NewInectionsPer100k { get; set; }
    public double AidsDeathsPer100k { get; set; }
    public double HealthSystemIndex { get; set; }

    // Foreign key
    public Country? Country { get; set; }
}