using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UseCase1.Enums;
using UseCase1.Models;

namespace  UseCase1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase {
  private readonly IHttpClientWrapper _client;

  public CountryController(IHttpClientWrapper client) {
    _client = client;
  }

  [HttpGet]
  public async Task<IActionResult> Get(string? countryName, int? population, SortOrder? sort, int? firstCountries) {
    var response = await _client.GetAsync("https://restcountries.com/v3.1/all");

    if (!response.IsSuccessStatusCode) {
      return StatusCode((int)response.StatusCode);
    }

    var jsonString = await response.Content.ReadAsStringAsync();
    var countries = JsonSerializer.Deserialize<List<Country>>(jsonString);

    if (countries is null) {
      return NotFound();
    }

    if (!string.IsNullOrEmpty(countryName)) {
      countries = FilterByName(countries, countryName);
    }

    // Filter by population
    if (population.HasValue) {
      countries = FilterByPopulation(countries, population.Value);
    }

    // Sort
    if (sort.HasValue) {
      countries = SortCountries(countries, sort.Value);
    }

    // Select first rows
    if (firstCountries.HasValue) {
      countries = SelectCountries(countries, firstCountries.Value);
    }

    return Ok(countries);
  }

  private static List<Country> FilterByName(IEnumerable<Country> countries, string nameFilter) {
    return countries.Where(c =>
      c.Name.Common != null && c.Name.Common.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
  }

  private static List<Country> FilterByPopulation(IEnumerable<Country> countries, long populationLimit) {
    return countries.Where(c => c.Population < populationLimit * 1_000_000).ToList();
  }
  
  private static List<Country> SortCountries(List<Country> countries, SortOrder sort) {
    countries = sort switch {
      SortOrder.Ascending => countries.OrderBy(c => c.Name.Common).ToList(),
      SortOrder.Descending => countries.OrderByDescending(c => c.Name.Common).ToList(),
      _ => countries
    };

    return countries;
  }

  private static List<Country> SelectCountries(List<Country> countries, int firstCountries) {
    return countries.Take(firstCountries).ToList();
  }
}