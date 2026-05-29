namespace AhfHivApi.Models;

public class ProgrammePerformance
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public int Year { get; set; }
    public int PatientsOnArt { get; set; }
    public double ViralSuppressionPct { get; set; }
    public int HtsTestsConducted { get; set; }
    public int PreventionReach { get; set; }
    public double ProgrammeTargetPct { get; set; }

    // Foreign key
    public Country? Country { get; set; }
}