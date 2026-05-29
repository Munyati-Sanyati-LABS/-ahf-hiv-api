using Microsoft.AspNetCore.Mvc;
using AhfHivApi.Services;

namespace AhfHivApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HivStatsController : ControllerBase
{
    private readonly IHivStatsService _hivStatsService;

    public HivStatsController(IHivStatsService hivStatsService)
    {
        _hivStatsService = hivStatsService;
    }

    // GET /api/hivstats
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stats = await _hivStatsService.GetAllAsync();
        return Ok(stats);
    }

    // GET /api/hivstats/ZAF
    [HttpGet("{countryCode}")]
    public async Task<IActionResult> GetByCountry(string countryCode)
    {
        var stats = await _hivStatsService.GetByCountryAsync(countryCode);
        return Ok(stats);
    }

    // GET /api/hivstats/year/2023
    [HttpGet("year/{year}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        var stats = await _hivStatsService.GetByYearAsync(year);
        return Ok(stats);
    }

    // GET /api/hivstats/ZAF/latest
    [HttpGet("{countryCode}/latest")]
    public async Task<IActionResult> GetLatest(string countryCode)
    {
        var stat = await _hivStatsService.GetLatestByCountryAsync(countryCode);
        if (stat == null)
            return NotFound(new { message = $"No data found for {countryCode}" });
        return Ok(stat);
    }
}