namespace UseCase1.Models; 

public class Demonyms
{
  [JsonPropertyName("eng")]
  public Demonym Eng { get; set; }

  [JsonPropertyName("fra")]
  public Demonym Fra { get; set; }
}

public class Demonym
{
  [JsonPropertyName("f")]
  public string Female { get; set; }

  [JsonPropertyName("m")]
  public string Male { get; set; }
}