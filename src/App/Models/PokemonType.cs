using System.Text.Json.Serialization;

namespace App.Models
{
    public record PokemonType
    {
        [JsonPropertyName("externalId")]
        public int? ExternalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
