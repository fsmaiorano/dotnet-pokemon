namespace Domain.Entities;

public class SpriteEntity
{
    public Guid Id { get; set; }
    public Guid PokemonId { get; set; }
    public virtual PokemonEntity? Pokemon { get; set; }
    public int ExternalId { get; private set; }
    public string BackDefault { get; private set; }
    public string BackFemale { get; private set; }
    public string FrontDefault { get; private set; }
    public string FrontFemale { get; private set; }
    public string BackShiny { get; private set; }
    public string BackShinyFemale { get; private set; }
    public string FrontShiny { get; private set; }
    public string FrontShinyFemale { get; private set; }
    public string DreamWorldFrontDefault { get; private set; }
    public string DreamWorldFrontFemale { get; private set; }
    public string HomeFrontDefault { get; private set; }
    public string HomeFrontFemale { get; private set; }
    public string HomeFrontShiny { get; private set; }
    public string HomeFrontShinyFemale { get; private set; }
    public string OfficialArtworkFrontDefault { get; private set; }
    public string OfficialArtworkFrontShiny { get; private set; }

    public SpriteEntity(int externalId, string backDefault, string backFemale, string frontDefault, string frontFemale, string backShiny, string backShinyFemale, string frontShiny, string frontShinyFemale, string dreamWorldFrontDefault, string dreamWorldFrontFemale, string homeFrontDefault, string homeFrontFemale, string homeFrontShiny, string homeFrontShinyFemale, string officialArtworkFrontDefault, string officialArtworkFrontShiny)
    {
        ExternalId = externalId;
        BackDefault = backDefault;
        BackFemale = backFemale;
        FrontDefault = frontDefault;
        FrontFemale = frontFemale;
        BackShiny = backShiny;
        BackShinyFemale = backShinyFemale;
        FrontShiny = frontShiny;
        FrontShinyFemale = frontShinyFemale;
        DreamWorldFrontDefault = dreamWorldFrontDefault;
        DreamWorldFrontFemale = dreamWorldFrontFemale;
        HomeFrontDefault = homeFrontDefault;
        HomeFrontFemale = homeFrontFemale;
        HomeFrontShiny = homeFrontShiny;
        HomeFrontShinyFemale = homeFrontShinyFemale;
        OfficialArtworkFrontDefault = officialArtworkFrontDefault;
        OfficialArtworkFrontShiny = officialArtworkFrontShiny;
    }
}
