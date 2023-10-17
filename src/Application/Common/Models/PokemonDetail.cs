using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.Common.Models;

public class PokemonDetail
{
    public int ExternalId { get; set; }
    [JsonPropertyName("height")]
    public int Height { get; set; }
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    [JsonPropertyName("sprites")]
    public PokemonSprite? Sprites { get; set; }
    [JsonPropertyName("types")]
    public List<TypeEntity>? Types { get; set; }
    [JsonPropertyName("evolves_from_species")]
    public int? EvolvesFrom { get; set; }
}
