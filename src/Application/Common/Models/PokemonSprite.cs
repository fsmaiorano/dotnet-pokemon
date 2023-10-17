using System.Text.Json.Serialization;

namespace Application.Common.Models;

public class PokemonSprite
{
    [JsonPropertyName("back_default")]
    public string? BackDefault { get; set; }
    [JsonPropertyName("back_female")]
    public string? BackFemale { get; set; }
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
    [JsonPropertyName("front_female")]
    public string? FrontFemale { get; set; }
    [JsonPropertyName("back_shiny")]
    public string? BackShiny { get; set; }
    [JsonPropertyName("back_shiny_female")]
    public string? BackShinyFemale { get; set; }
    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }
    [JsonPropertyName("front_shiny_female")]
    public string? FrontShinyFemale { get; set; }
    [JsonPropertyName("other")]
    public Other? Others { get; set; }

    public class Other
    {
        [JsonPropertyName("dream_world")]
        public DreamWorld? DreamWorld { get; set; }

        [JsonPropertyName("home")]
        public Home? Home { get; set; }

        [JsonPropertyName("official-artwork")]
        public OfficialArtwork? OfficialArtwork { get; set; }
    }

    public class DreamWorld
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("front_female")]
        public string? FrontFemale { get; set; }
    }

    public class Home
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("front_female")]
        public string? FrontFemale { get; set; }

        [JsonPropertyName("front_shiny")]
        public string? FrontShiny { get; set; }

        [JsonPropertyName("front_shiny_female")]
        public string? FrontShinyFemale { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("front_shiny")]
        public string? FrontShiny { get; set; }
    }
}

