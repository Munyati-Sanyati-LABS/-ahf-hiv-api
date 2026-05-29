namespace AhfHivApi.Models;

public class Country
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool AhfPresence { get; set; }

    // Navigation properties
    public ICollection<HivHealthIndicator> HivHealthIndicators { get; set; } = new List<HivHealthIndicator>();
    public ICollection<ClimateRiskIndicator> ClimateRiskIndicators { get; set; } = new List<ClimateRiskIndicator>();
    public ICollection<ProgrammePerformance> ProgrammePerformances { get; set; } = new List<ProgrammePerformance>();
}
