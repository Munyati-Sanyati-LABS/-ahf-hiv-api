using Microsoft.EntityFrameworkCore;
using AhfHivApi.Data;
using AhfHivApi.Models;

namespace AhfHivApi.Services;

public class ClimateService : IClimateService
{
    private readonly AppDbContext _context;

    public ClimateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClimateRiskIndicator>> GetAllAsync()
    {
        return await _context.ClimateRiskIndicators
            .Include(c => c.Country)
            .OrderBy(c => c.CountryCode)
            .ThenBy(c => c.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<ClimateRiskIndicator>> GetByCountryAsync(string countryCode)
    {
        return await _context.ClimateRiskIndicators
            .Include(c => c.Country)
            .Where(c => c.CountryCode == countryCode.ToUpper())
            .OrderBy(c => c.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<ClimateRiskIndicator>> GetHighRiskCountriesAsync(double threshold)
    {
        return await _context.ClimateRiskIndicators
            .Include(c => c.Country)
            .Where(c => c.ClimateVulnerabilityIndex >= threshold)
            .OrderByDescending(c => c.ClimateVulnerabilityIndex)
            .ToListAsync();
    }
}