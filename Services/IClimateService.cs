using AhfHivApi.Models;

namespace AhfHivApi.Services;

public interface IClimateService
{
    Task<IEnumerable<ClimateRiskIndicator>> GetAllAsync();
    Task<IEnumerable<ClimateRiskIndicator>> GetByCountryAsync(string countryCode);
    Task<IEnumerable<ClimateRiskIndicator>> GetHighRiskCountriesAsync(double threshold);
}