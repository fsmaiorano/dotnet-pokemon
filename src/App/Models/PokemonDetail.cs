using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;

namespace App.Models;

public class PokemonDetail
{
    [JsonPropertyName("externalId")]
    public int? ExternalId { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("weight")]
    public int? Weight { get; set; }

    [JsonPropertyName("evolvesFromPokemonExternalId")]
    public int? EvolvesFromPokemonExternalId { get; set; }
}
