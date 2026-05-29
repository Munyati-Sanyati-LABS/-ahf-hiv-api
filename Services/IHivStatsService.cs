using AhfHivApi.Models;

namespace AhfHivApi.Services;

public interface IHivStatsService
{
    Task<IEnumerable<HivHealthIndicator>> GetAllAsync();
    Task<IEnumerable<HivHealthIndicator>> GetByCountryAsync(string countryCode);
    Task<IEnumerable<HivHealthIndicator>> GetByYearAsync(int year);
    Task<HivHealthIndicator?> GetLatestByCountryAsync(string countryCode);
}