using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using AhfHivApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AhfHivApi.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Only seed if database is empty
        if (await context.Countries.AnyAsync()) return;

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };

        // ── SEED COUNTRIES ──────────────────────────────────────────────
        var countriesPath = Path.Combine(basePath, "dim_countries.csv");
        if (File.Exists(countriesPath))
        {
            using var reader = new StreamReader(countriesPath);
            using var csv = new CsvReader(reader, csvConfig);
            var records = csv.GetRecords<CountryCsv>().ToList();
            var countries = records.Select(r => new Country
            {
                CountryCode = r.country_code,
                CountryName = r.country,
                Region = r.region,
                Latitude = r.latitude,
                Longitude = r.longitude,
                AhfPresence = r.ahf_presence.ToString().ToLower() == "true"
            }).ToList();
            await context.Countries.AddRangeAsync(countries);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {countries.Count} countries");
        }

        // ── SEED HIV HEALTH ─────────────────────────────────────────────
        var hivPath = Path.Combine(basePath, "fact_hiv_health.csv");
        if (File.Exists(hivPath))
        {
            using var reader = new StreamReader(hivPath);
            using var csv = new CsvReader(reader, csvConfig);
            var records = csv.GetRecords<HivHealthCsv>().ToList();
            var indicators = records.Select(r => new HivHealthIndicator
            {
                CountryCode = r.country_code,
                Year = r.year,
                HivPrevalencePct = r.hiv_prevalence_pct,
                ArtCoveragePct = r.art_coverage_pct,
                NewInectionsPer100k = r.new_infections_per100k,
                AidsDeathsPer100k = r.aids_deaths_per100k,
                HealthSystemIndex = r.health_system_index
            }).ToList();
            await context.HivHealthIndicators.AddRangeAsync(indicators);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {indicators.Count} HIV health records");
        }

        // ── SEED CLIMATE RISK ───────────────────────────────────────────
        var climatePath = Path.Combine(basePath, "fact_climate_risk.csv");
        if (File.Exists(climatePath))
        {
            using var reader = new StreamReader(climatePath);
            using var csv = new CsvReader(reader, csvConfig);
            var records = csv.GetRecords<ClimateRiskCsv>().ToList();
            var indicators = records.Select(r => new ClimateRiskIndicator
            {
                CountryCode = r.country_code,
                Year = r.year,
                AvgTempCelsius = r.avg_temp_celsius,
                RainfallAnomalyPct = r.rainfall_anomaly_pct,
                FloodEvents = r.flood_events,
                DroughtEvents = r.drought_events,
                ClimateVulnerabilityIndex = r.climate_vulnerability_index,
                ExtremeHeatDays = r.extreme_heat_days
            }).ToList();
            await context.ClimateRiskIndicators.AddRangeAsync(indicators);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {indicators.Count} climate risk records");
        }

        // ── SEED PROGRAMME PERFORMANCE ──────────────────────────────────
        var progPath = Path.Combine(basePath, "fact_programme_performance.csv");
        if (File.Exists(progPath))
        {
            using var reader = new StreamReader(progPath);
            using var csv = new CsvReader(reader, csvConfig);
            var records = csv.GetRecords<ProgrammeCsv>().ToList();
            var performances = records.Select(r => new ProgrammePerformance
            {
                CountryCode = r.country_code,
                Year = r.year,
                PatientsOnArt = r.patients_on_art,
                ViralSuppressionPct = r.viral_suppression_pct,
                HtsTestsConducted = r.hts_tests_conducted,
                PreventionReach = r.prevention_reach,
                ProgrammeTargetPct = r.programme_target_pct
            }).ToList();
            await context.ProgrammePerformances.AddRangeAsync(performances);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {performances.Count} programme performance records");
        }
    }
}

// ── CSV MAPPING CLASSES ──────────────────────────────────────────────────────
public class CountryCsv
{
    public string country_code { get; set; } = string.Empty;
    public string country { get; set; } = string.Empty;
    public string region { get; set; } = string.Empty;
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string ahf_presence { get; set; } = string.Empty;
}

public class HivHealthCsv
{
    public string country_code { get; set; } = string.Empty;
    public int year { get; set; }
    public double hiv_prevalence_pct { get; set; }
    public double art_coverage_pct { get; set; }
    public double new_infections_per100k { get; set; }
    public double aids_deaths_per100k { get; set; }
    public double health_system_index { get; set; }
}

public class ClimateRiskCsv
{
    public string country_code { get; set; } = string.Empty;
    public int year { get; set; }
    public double avg_temp_celsius { get; set; }
    public double rainfall_anomaly_pct { get; set; }
    public int flood_events { get; set; }
    public int drought_events { get; set; }
    public double climate_vulnerability_index { get; set; }
    public int extreme_heat_days { get; set; }
}

public class ProgrammeCsv
{
    public string country_code { get; set; } = string.Empty;
    public int year { get; set; }
    public int patients_on_art { get; set; }
    public double viral_suppression_pct { get; set; }
    public int hts_tests_conducted { get; set; }
    public int prevention_reach { get; set; }
    public double programme_target_pct { get; set; }
}