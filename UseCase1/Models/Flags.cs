namespace UseCase1.Models; 


public class Flags
{
  [JsonPropertyName("png")]
  public string Png { get; set; }

  [JsonPropertyName("svg")]
  public string Svg { get; set; }
}