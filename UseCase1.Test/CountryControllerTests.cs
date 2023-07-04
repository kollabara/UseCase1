using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using UseCase1.Controllers;
using UseCase1.Enums;
using UseCase1.Models;
using Xunit;

namespace UseCase1.Test; 

public class CountryControllerTests
{
    private readonly CountryController _controller;

    public CountryControllerTests()
    {
        var httpClientFactoryMock = new Mock<IHttpClientWrapper>();
        
        var countries = GetSampleCountries();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(GetSerializedJson(countries), Encoding.UTF8, "application/json")
        };
        
        httpClientFactoryMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

        _controller = new CountryController(httpClientFactoryMock.Object);
    }
    
    private string GetSerializedJson(object obj)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        JsonSerializer.Serialize(writer, obj, options);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static List<Country> GetSampleCountries()
    {
        // Create sample countries for testing
        return new List<Country>
        {
            new() { Name = new Name { Common = "Country 1" }, Population = 100 },
            new() { Name = new Name { Common = "Country 2" }, Population = 200 },
            new() { Name = new Name { Common = "Country 3" }, Population = 300 }
        };
    }

    [Fact]
    public async Task Get_WithCountryName_ReturnsFilteredCountries()
    {
        // Arrange
        var countries = GetSampleCountries();
        var filteredName = "Country 1";
        var expectedCountries = countries.Where(c => c.Name.Common == filteredName).ToList();

        // Act
        var result = await _controller.Get(filteredName, null, null, null) as ObjectResult;
        var actualCountries = result?.Value as List<Country>;

        // Assert
        Assert.NotNull(actualCountries);
        Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
        Assert.Equal(expectedCountries.Count, actualCountries.Count);
        Assert.All(actualCountries, c => Assert.Equal(filteredName, c.Name.Common));
    }

    [Fact]
    public async Task Get_WithPopulation_ReturnsFilteredCountries()
    {
        // Arrange
        var countries = GetSampleCountries();
        var populationLimit = 150;
        var expectedCountries = countries.Where(c => c.Population < populationLimit * 1_000_000).ToList();
        
        // Act
        var result = await _controller.Get(null, populationLimit, null, null) as ObjectResult;
        var actualCountries = result?.Value as List<Country>;

        // Assert
        Assert.NotNull(actualCountries);
        Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
        Assert.Equal(expectedCountries.Count, actualCountries.Count);
        Assert.All(actualCountries, c => Assert.True(c.Population < populationLimit * 1_000_000));
    }

    [Fact]
    public async Task Get_WithSortOrder_ReturnsSortedCountries()
    {
        // Arrange
        var countries = GetSampleCountries();
        var sortOrder = SortOrder.Descending;
        var expectedCountries = countries.OrderByDescending(c => c.Name.Common).ToList();

        // Act
        var result = await _controller.Get(null, null, sortOrder, null) as ObjectResult;
        var actualCountries = result?.Value as List<Country>;

        // Assert
        Assert.NotNull(actualCountries);
        Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
        Assert.Equal(expectedCountries.Count, actualCountries.Count);
        Assert.Equivalent(expectedCountries, actualCountries);
    }

    [Fact]
    public async Task Get_WithFirstRows_ReturnsSelectedCountries()
    {
        // Arrange
        var countries = GetSampleCountries();
        var firstRows = 2;
        var expectedCountries = countries.Take(firstRows).ToList();
        
        // Act
        var result = await _controller.Get(null, null, null, firstRows) as ObjectResult;
        var actualCountries = result?.Value as List<Country>;

        // Assert
        Assert.NotNull(actualCountries);
        Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
        Assert.Equal(expectedCountries.Count, actualCountries.Count);
        Assert.Equivalent(expectedCountries, actualCountries);
    }
}