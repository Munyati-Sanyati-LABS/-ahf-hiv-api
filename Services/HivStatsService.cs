using Microsoft.EntityFrameworkCore;
using AhfHivApi.Data;
using AhfHivApi.Models;

namespace AhfHivApi.Services;

public class HivStatsService : IHivStatsService
{
    private readonly AppDbContext _context;

    public HivStatsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HivHealthIndicator>> GetAllAsync()
    {
        return await _context.HivHealthIndicators
            .Include(h => h.Country)
            .OrderBy(h => h.CountryCode)
            .ThenBy(h => h.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<HivHealthIndicator>> GetByCountryAsync(string countryCode)
    {
        return await _context.HivHealthIndicators
            .Include(h => h.Country)
            .Where(h => h.CountryCode == countryCode.ToUpper())
            .OrderBy(h => h.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<HivHealthIndicator>> GetByYearAsync(int year)
    {
        return await _context.HivHealthIndicators
            .Include(h => h.Country)
            .Where(h => h.Year == year)
            .OrderByDescending(h => h.HivPrevalencePct)
            .ToListAsync();
    }

    public async Task<HivHealthIndicator?> GetLatestByCountryAsync(string countryCode)
    {
        return await _context.HivHealthIndicators
            .Include(h => h.Country)
            .Where(h => h.CountryCode == countryCode.ToUpper())
            .OrderByDescending(h => h.Year)
            .FirstOrDefaultAsync();
    }
}