using Microsoft.EntityFrameworkCore;
using AhfHivApi.Data;
using AhfHivApi.Models;

namespace AhfHivApi.Services;

public class ProgrammeService : IProgrammeService
{
    private readonly AppDbContext _context;

    public ProgrammeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProgrammePerformance>> GetAllAsync()
    {
        return await _context.ProgrammePerformances
            .Include(p => p.Country)
            .OrderBy(p => p.CountryCode)
            .ThenBy(p => p.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProgrammePerformance>> GetByCountryAsync(string countryCode)
    {
        return await _context.ProgrammePerformances
            .Include(p => p.Country)
            .Where(p => p.CountryCode == countryCode.ToUpper())
            .OrderBy(p => p.Year)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProgrammePerformance>> GetLatestAllCountriesAsync()
    {
        var allData = await _context.ProgrammePerformances
            .Include(p => p.Country)
            .OrderBy(p => p.CountryCode)
            .ThenByDescending(p => p.Year)
            .ToListAsync();

        return allData
            .GroupBy(p => p.CountryCode)
            .Select(g => g.First())
            .OrderByDescending(p => p.PatientsOnArt)
            .ToList();
    }
}