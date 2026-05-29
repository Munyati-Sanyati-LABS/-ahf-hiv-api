using Microsoft.AspNetCore.Mvc;
using AhfHivApi.Services;

namespace AhfHivApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    // GET /api/countries
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllAsync();
        return Ok(countries);
    }

    // GET /api/countries/ZAF
    [HttpGet("{countryCode}")]
    public async Task<IActionResult> GetByCode(string countryCode)
    {
        var country = await _countryService.GetByCodeAsync(countryCode);
        if (country == null)
            return NotFound(new { message = $"Country {countryCode} not found" });
        return Ok(country);
    }

    // GET /api/countries/region/Southern Africa
    [HttpGet("region/{region}")]
    public async Task<IActionResult> GetByRegion(string region)
    {
        var countries = await _countryService.GetByRegionAsync(region);
        return Ok(countries);
    }
}