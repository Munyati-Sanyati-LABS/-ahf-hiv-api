using AhfHivApi.Models;

namespace AhfHivApi.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByCodeAsync(string countryCode);
    Task<IEnumerable<Country>> GetByRegionAsync(string region);
}