using Microsoft.EntityFrameworkCore;
using AhfHivApi.Data;
using AhfHivApi.Models;

namespace AhfHivApi.Services;

public class CountryService : ICountryService
{
    private readonly AppDbContext _context;

    public CountryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries
            .OrderBy(c => c.Region)
            .ThenBy(c => c.CountryName)
            .ToListAsync();
    }

    public async Task<Country?> GetByCodeAsync(string countryCode)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(c => c.CountryCode == countryCode.ToUpper());
    }

    public async Task<IEnumerable<Country>> GetByRegionAsync(string region)
    {
        return await _context.Countries
            .Where(c => c.Region.ToLower() == region.ToLower())
            .OrderBy(c => c.CountryName)
            .ToListAsync();
    }
}