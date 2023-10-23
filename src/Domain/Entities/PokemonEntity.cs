using Domain.Common;

namespace Domain.Entities;

public class PokemonEntity : BaseEntity
{
    public required int ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public PokemonDetailEntity? PokemonDetail { get; set; }
    public SpriteEntity? Sprites { get; set; }
    public List<TypeEntity>? Types { get; set; }
    public List<AbilityEntity>? Abilities { get; set; }
    public List<MoveEntity>? Moves { get; set; }
}
