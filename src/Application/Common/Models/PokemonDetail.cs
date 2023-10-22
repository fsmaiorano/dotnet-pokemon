using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models;

public class PokemonDetail
{
    [JsonPropertyName("id")]
    public int ExternalId { get; set; }
    [JsonPropertyName("height")]
    public int Height { get; set; }
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    [JsonPropertyName("sprites")]
    public PokemonSprite? Sprites { get; set; }
    [JsonPropertyName("types")]
    public List<PokemonType>? Types { get; set; }
    [JsonPropertyName("evolves_from_species")]
    public int? EvolvesFrom { get; set; }

    public PokemonDetail()
    {
        Types = new();
        Sprites = new();
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PokemonDetail, PokemonDetailEntity>().ReverseMap();

            // CreateMap<GenericContent, AbilityEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            // CreateMap<GenericContent, PokemonEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            // CreateMap<GenericContent, TypeEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            // CreateMap<GenericContent, MoveEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
        }
    }
}
