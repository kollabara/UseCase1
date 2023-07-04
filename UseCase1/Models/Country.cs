namespace UseCase1.Models;

public class NativeNameLanguage
{
  [JsonPropertyName("official")]
  public string Official { get; set; }

  [JsonPropertyName("common")]
  public string Common { get; set; }
}

public class Name
{
  [JsonPropertyName("common")]
  public string Common { get; set; }

  [JsonPropertyName("official")]
  public string Official { get; set; }

  [JsonPropertyName("nativeName")]
  public Dictionary<string, NativeNameLanguage> NativeName { get; set; }
}

public class Country
{
    [JsonPropertyName("cca2")]
    public string Cca2 { get; set; }

    [JsonPropertyName("cca3")]
    public string Cca3 { get; set; }

    [JsonPropertyName("ccn3")]
    public string Ccn3 { get; set; }

    [JsonPropertyName("cioc")]
    public string Cioc { get; set; }

    [JsonPropertyName("independent")]
    public bool Independent { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("unMember")]
    public bool UnMember { get; set; }

    [JsonPropertyName("currencies")]
    public Dictionary<string, Currency> Currencies { get; set; }

    [JsonPropertyName("capital")]
    public List<string> Capital { get; set; }

    [JsonPropertyName("altSpellings")]
    public List<string> AltSpellings { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("subregion")]
    public string Subregion { get; set; }

    [JsonPropertyName("languages")]
    public Dictionary<string, string> Languages { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("area")]
    public double Area { get; set; }

    [JsonPropertyName("borders")]
    public List<string> Borders { get; set; }

    [JsonPropertyName("population")]
    public long Population { get; set; }

    [JsonPropertyName("flags")]
    public Flags Flags { get; set; }

    [JsonPropertyName("name")]
    public Name Name { get; set; }

    [JsonPropertyName("demonyms")]
    public Demonyms Demonyms { get; set; }

    [JsonPropertyName("areaRank")]
    public int AreaRank { get; set; }

    [JsonPropertyName("populationRank")]
    public int PopulationRank { get; set; }
}
