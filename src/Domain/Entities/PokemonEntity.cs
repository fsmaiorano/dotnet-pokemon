using Domain.Common;

namespace Domain.Entities;

public class PokemonEntity
{
    public Guid Id { get; set; }
    public int ExternalId { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }
    public virtual PokemonDetailEntity? PokemonDetail { get; set; }
    public virtual SpriteEntity? Sprites { get; set; }
    public virtual IList<TypeEntity>? Types { get; set; }
    public virtual IList<AbilityEntity>? Abilities { get; set; }
    public virtual IList<MoveEntity>? Moves { get; set; }

    public PokemonEntity(int externalId, string name, string url)
    {
        ExternalId = externalId;
        Name = name;
        Url = url;
    }
}
