using System.Text.Json.Serialization;

namespace Application.Common.Models;
public class PokemonSpecie
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("base_happiness")]
    public int? BaseHappiness { get; set; }

    [JsonPropertyName("capture_rate")]
    public int? CaptureRate { get; set; }

    [JsonPropertyName("color")]
    public GenericContent? Color { get; set; }

    [JsonPropertyName("habitat")]
    public GenericContent? Habitat { get; set; }

    [JsonPropertyName("has_gender_differences")]
    public bool HasGenderDifferences { get; set; }

    [JsonPropertyName("hatch_counter")]
    public int? HatchCounter { get; set; }

    [JsonPropertyName("is_baby")]
    public bool IsBaby { get; set; }

    [JsonPropertyName("is_legendary")]
    public bool IsLegendary { get; set; }

    [JsonPropertyName("is_mythical")]
    public bool IsMythical { get; set; }

    [JsonPropertyName("evolves_from_species")]
    public GenericContent? EvolvesFromSpecies { get; set; }
}

