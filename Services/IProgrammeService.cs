using AhfHivApi.Models;

namespace AhfHivApi.Services;

public interface IProgrammeService
{
    Task<IEnumerable<ProgrammePerformance>> GetAllAsync();
    Task<IEnumerable<ProgrammePerformance>> GetByCountryAsync(string countryCode);
    Task<IEnumerable<ProgrammePerformance>> GetLatestAllCountriesAsync();
}