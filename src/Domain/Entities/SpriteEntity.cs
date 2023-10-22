namespace Domain.Entities;

public class SpriteEntity
{
    public Guid Id { get; set; }
    public Guid PokemonId { get; set; }
    public virtual PokemonEntity? Pokemon { get; set; }
    public int ExternalId { get; set; }
    public string? BackDefault { get; set; }
    public string? BackFemale { get; set; }
    public string? FrontDefault { get; set; }
    public string? FrontFemale { get; set; }
    public string? BackShiny { get; set; }
    public string? BackShinyFemale { get; set; }
    public string? FrontShiny { get; set; }
    public string? FrontShinyFemale { get; set; }
    public string? DreamWorldFrontDefault { get; set; }
    public string? DreamWorldFrontFemale { get; set; }
    public string? HomeFrontDefault { get; set; }
    public string? HomeFrontFemale { get; set; }
    public string? HomeFrontShiny { get; set; }
    public string? HomeFrontShinyFemale { get; set; }
    public string? OfficialArtworkFrontDefault { get; set; }
    public string? OfficialArtworkFrontShiny { get; set; }
}
