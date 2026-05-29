using Microsoft.AspNetCore.Mvc;
using AhfHivApi.Services;

namespace AhfHivApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgrammePerformanceController : ControllerBase
{
    private readonly IProgrammeService _programmeService;

    public ProgrammePerformanceController(IProgrammeService programmeService)
    {
        _programmeService = programmeService;
    }

    // GET /api/programmeperformance
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _programmeService.GetAllAsync();
        return Ok(data);
    }

    // GET /api/programmeperformance/ZAF
    [HttpGet("{countryCode}")]
    public async Task<IActionResult> GetByCountry(string countryCode)
    {
        var data = await _programmeService.GetByCountryAsync(countryCode);
        return Ok(data);
    }

    // GET /api/programmeperformance/latest
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestAllCountries()
    {
        var data = await _programmeService.GetLatestAllCountriesAsync();
        return Ok(data);
    }
}