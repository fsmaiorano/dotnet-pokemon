using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public record PokemonType
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
        [JsonPropertyName("type")]
        public TypeObject? Type { get; set; }

        public class TypeObject
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
            [JsonPropertyName("url")]
            public string? Url { get; set; }
        }

        public PokemonType()
        {
            Type = new();
        }
    }
}
