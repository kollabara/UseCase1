using Microsoft.AspNetCore.Mvc;
using UseCase1.Enums;
using UseCase1.Models;

namespace  UseCase1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
  private readonly HttpClient _client;

  public CountryController(IHttpClientFactory clientFactory)
  {
    _client = clientFactory.CreateClient();
  }

  [HttpGet]
  public async Task<IActionResult> Get(string? countryName, long? population, SortOrder? sort, int? firstRows)
  {
    var response = await _client.GetAsync("https://restcountries.com/v3.1/all");

    if (!response.IsSuccessStatusCode) {
      return StatusCode((int)response.StatusCode);
    }
    
    var content = await response.Content.ReadAsStringAsync();
    var countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(content);

    // Filter by country name
    if (!string.IsNullOrEmpty(countryName))
    {
      countries = countries.Where(c => c.Name.Common == countryName).ToList();
    }

    // Filter by population
    if (population.HasValue)
    {
      countries = countries.Where(c => c.Population == population.Value).ToList();
    }

    // Sort
    if (sort.HasValue)
    {
      countries = sort == SortOrder.Ascending
        ? countries.OrderBy(c => c.Population).ToList()
        : countries.OrderByDescending(c => c.Population).ToList();
    }

    // Select first rows
    if (firstRows.HasValue)
    {
      countries = countries.Take(firstRows.Value).ToList();
    }

    return Ok(countries);
  }
}