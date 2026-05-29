using Microsoft.AspNetCore.Mvc;
using AhfHivApi.Services;

namespace AhfHivApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClimateRiskController : ControllerBase
{
    private readonly IClimateService _climateService;

    public ClimateRiskController(IClimateService climateService)
    {
        _climateService = climateService;
    }

    // GET /api/climaterisk
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _climateService.GetAllAsync();
        return Ok(data);
    }

    // GET /api/climaterisk/ZAF
    [HttpGet("{countryCode}")]
    public async Task<IActionResult> GetByCountry(string countryCode)
    {
        var data = await _climateService.GetByCountryAsync(countryCode);
        return Ok(data);
    }

    // GET /api/climaterisk/highrisk?threshold=70
    [HttpGet("highrisk")]
    public async Task<IActionResult> GetHighRisk([FromQuery] double threshold = 70)
    {
        var data = await _climateService.GetHighRiskCountriesAsync(threshold);
        return Ok(data);
    }
}