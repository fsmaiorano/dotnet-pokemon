using System.Text.Json.Serialization;

namespace App.Models;

public class Pokemon
{
    [JsonPropertyName("externalId")]
    public int? ExternalId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("pokemonDetail")]
    public PokemonDetail PokemonDetail { get; set; }

    [JsonPropertyName("sprites")]
    public PokemonSprite Sprites { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonType> Types { get; set; }

    [JsonPropertyName("abilities")]
    public object Abilities { get; set; }

    [JsonPropertyName("moves")]
    public object Moves { get; set; }
}
