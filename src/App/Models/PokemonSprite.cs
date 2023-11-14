using System.Text.Json.Serialization;

namespace App.Models;

public class PokemonSprite
{
    [JsonPropertyName("externalId")]
    public int? ExternalId { get; set; }

    [JsonPropertyName("backDefault")]
    public string BackDefault { get; set; }

    [JsonPropertyName("backFemale")]
    public string BackFemale { get; set; }

    [JsonPropertyName("frontDefault")]
    public string FrontDefault { get; set; }

    [JsonPropertyName("frontFemale")]
    public string FrontFemale { get; set; }

    [JsonPropertyName("backShiny")]
    public string BackShiny { get; set; }

    [JsonPropertyName("backShinyFemale")]
    public string BackShinyFemale { get; set; }

    [JsonPropertyName("frontShiny")]
    public string FrontShiny { get; set; }

    [JsonPropertyName("frontShinyFemale")]
    public string FrontShinyFemale { get; set; }

    [JsonPropertyName("dreamWorldFrontDefault")]
    public string DreamWorldFrontDefault { get; set; }

    [JsonPropertyName("dreamWorldFrontFemale")]
    public object DreamWorldFrontFemale { get; set; }

    [JsonPropertyName("homeFrontDefault")]
    public string HomeFrontDefault { get; set; }

    [JsonPropertyName("homeFrontFemale")]
    public string HomeFrontFemale { get; set; }

    [JsonPropertyName("homeFrontShiny")]
    public string HomeFrontShiny { get; set; }

    [JsonPropertyName("homeFrontShinyFemale")]
    public string HomeFrontShinyFemale { get; set; }

    [JsonPropertyName("officialArtworkFrontDefault")]
    public string OfficialArtworkFrontDefault { get; set; }

    [JsonPropertyName("officialArtworkFrontShiny")]
    public string OfficialArtworkFrontShiny { get; set; }
}

